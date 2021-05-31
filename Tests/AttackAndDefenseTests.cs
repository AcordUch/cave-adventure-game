using NUnit.Framework;

namespace Cave_Adventure
{
    [TestFixture]
    public class AttackAndDefenseTests
    {
        private ArenaMap _arena;
        private Player _player;
        private Monster _snake;
        private Monster _spider;

        [SetUp]
        public void Init()
        {
            var arena = new string[]
            {
                "P .Sn.",
                "Sp.  .",
            };
            _arena = ArenaMap.CreateNewArenaMap(arena);

            _player = _arena.Player;
            _snake = _arena.Monsters[0];
            _spider = _arena.Monsters[1];
        }

        [Test]
        public void OneAttackPerSnake() => OneAttackOnMonster(_snake, 2.5);

        [Test]
        public void OneAttackPerSpider() => OneAttackOnMonster(_spider, 0.0);

        [Test]
        public void SnakeKilledPlayer() => MonsterKilledPlayer(_snake, 29);

        [Test]
        public void SpiderKilledPlayer() => MonsterKilledPlayer(_spider, 46);

        [Test]
        public void CheckCharactersAPWithSpider()
        {
            _spider.Defending(_player);

            Assert.AreEqual(0, _player.AP);
            Assert.AreEqual(3, _spider.AP);

            _player.Defending(_spider);

            Assert.AreEqual(0, _player.AP);
            Assert.AreEqual(0, _spider.AP);
        }

        [Test]
        public void CheckCharactersAPWithSnake()
        {
            _snake.Defending(_player);

            Assert.AreEqual(0, _player.AP);
            Assert.AreEqual(2, _snake.AP);

            _player.Defending(_snake);

            Assert.AreEqual(0, _player.AP);
            Assert.AreEqual(0, _snake.AP);
        }

        [Test]
        public void FightSnakeWithSpider()
        {
            _snake.Defending(_spider);

            Assert.IsTrue(_snake.CheckIsAliveAndChangeState());
            Assert.AreEqual(20.0, _snake.Health);

            _spider.Defending(_snake);

            Assert.IsTrue(_spider.CheckIsAliveAndChangeState());
            Assert.AreEqual(17.0, _spider.Health);

            for (var i = 0; i < 5; i++)
            {
                _snake.Defending(_spider);
                _spider.Defending(_snake);
            }

            Assert.IsFalse(_snake.CheckIsAliveAndChangeState());
            Assert.IsTrue(_spider.CheckIsAliveAndChangeState());
        }

        private void OneAttackOnMonster(Monster monster, double expectedHealth)
        {
            monster.Defending(_player);

            Assert.AreEqual(expectedHealth, monster.Health);
        }

        private void MonsterKilledPlayer(Monster monster, int attacksNumber)
        {
            _player.Defending(monster);

            Assert.IsTrue(_player.CheckIsAliveAndChangeState());

            for (var i = 0; i < attacksNumber; i++)
                _player.Defending(monster);

            Assert.IsFalse(_player.CheckIsAliveAndChangeState());
        }
    }
}