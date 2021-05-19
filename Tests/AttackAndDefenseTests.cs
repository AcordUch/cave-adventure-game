using NUnit.Framework;

namespace Cave_Adventure
{
    [TestFixture]
    public class AttackAndDefenseTests
    {
        private ArenaMap _arena;
        private Player player;
        private Monster snake;
        private Monster spider;

        [SetUp]
        public void Init()
        {
            var arena = new string[]
            {
                "P .Sn.",
                "Sp.  .",
            };
            _arena = ArenaMap.CreateNewArenaMap(arena);

            player = _arena.Player;
            snake = _arena.Monsters[0];
            spider = _arena.Monsters[1];
        }

        [Test]
        public void OneAttackPerSnake() => OneAttackOnMonster(snake, 2.5);

        [Test]
        public void OneAttackPerSpider() => OneAttackOnMonster(spider, 0.0);

        [Test]
        public void SnakeKilledPlayer() => MonsterKilledPlayer(snake, 19);

        [Test]
        public void SpiderKilledPlayer() => MonsterKilledPlayer(spider, 46);

        [Test]
        public void CheckCharactersAP()
        {
            snake.Defending(player);
            spider.Defending(player);

            Assert.AreEqual(0, player.AP);
            Assert.AreEqual(2, snake.AP);
            Assert.AreEqual(2, spider.AP);
        }

        [Test]
        public void FightSnakeWithSpider()
        {
            snake.Defending(spider);

            Assert.IsTrue(snake.CheckIsAliveAndChangeState());
            Assert.AreEqual(20.5, snake.Health);

            spider.Defending(snake);

            Assert.IsTrue(spider.CheckIsAliveAndChangeState());
            Assert.AreEqual(14.75, spider.Health);

            for (var i = 0; i < 5; i++)
            {
                snake.Defending(spider);
                spider.Defending(snake);
            }

            Assert.IsFalse(snake.CheckIsAliveAndChangeState());
            Assert.IsFalse(spider.CheckIsAliveAndChangeState());
        }

        public void OneAttackOnMonster(Monster monster, double expectedHealth)
        {
            monster.Defending(player);

            Assert.AreEqual(expectedHealth, monster.Health);
        }

        public void MonsterKilledPlayer(Monster monster, int attacksNumber)
        {
            player.Defending(monster);

            Assert.IsTrue(player.CheckIsAliveAndChangeState());

            for (var i = 0; i < attacksNumber; i++)
                player.Defending(monster);

            Assert.IsFalse(player.CheckIsAliveAndChangeState());
        }
    }
}