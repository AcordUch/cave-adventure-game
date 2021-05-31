using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cave_Adventure
{
    [TestFixture]
    public class AStarTests
    {
        [Test]
        public void AStarPFNoPath()
        {
            var textArena = 
@"# .P .
Sp.# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, arena.Monsters[0].Position, 3);
            //AssertPaths(paths, arena, new []{1});
            Assert.AreEqual(new Point(-1, -1), paths.ToList()[^1]);
        }
        
        [Test]
        public void  AStarPFEmptyMapWithPlayer()
        {
            var textArena =
@"  .  .P .
  .  .  .
Sp.  .  .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, arena.Monsters[0].Position, 5).ToList();
            Assert.IsTrue(GlobalConst.PossibleDirections
                .Select(p => arena.Player.Position + p).Any(p => paths[^1] == p));
        }
        
        [Test]
        public void TestTest()
        {
            var textArena = Properties.Resources.Arena1Debug;
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, arena.Monsters[0].Position, 10).ToList();
            Assert.IsTrue(GlobalConst.PossibleDirections
                .Select(p => arena.Player.Position + p).Any(p => paths[^1] == p));
        }

        [Test]
        public void MonsterGoThroughAnotherMonster()
        {
            var textArena = 
@"# .# .# .# .# .# .# .
P .  .Sp.  .Sn.  .  .
# .# .# .# .# .# .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var monster = arena.Monsters.First(m => m is Snake);
            var paths = GetPaths(arena, monster.Position, 5, false).ToList();
            Assert.AreEqual(new Point(1, 1), paths[^1]);
        }
        
        [Test]
        public void MonsterDontStayInAnotherMonster1()
        {
            var textArena = 
@"# .# .# .# .# .# .# .
P .  .Sp.  .  .  .Sn.
# .# .# .# .# .# .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var monster = arena.Monsters.First(m => m is Snake);
            var paths = GetPaths(arena, monster.Position, 4, false).ToList();
            Assert.AreEqual(new Point(3, 1), paths[^1]);
        }
        
        [Test]
        public void MonsterDontStayInAnotherMonster2()
        {
            var textArena = 
@"# .# .# .# .# .# .# .
P .  .Sp.Sp.  .  .Sn.
# .# .# .# .# .# .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var monster = arena.Monsters.First(m => m is Snake);
            var paths = GetPaths(arena, monster.Position, 4, false).ToList();
            Assert.AreEqual(new Point(4, 1), paths[^1]);
        }
        
        [Test]
        public void MonsterDontStayInAnotherMonster3()
        {
            var textArena = 
@"# .# .# .# .# .# .# .
P .  .Sp.Sp.Sp.  .Sn.
# .# .# .# .# .# .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var monster = arena.Monsters.First(m => m is Snake);
            var paths = GetPaths(arena, monster.Position, 4, false).ToList();
            Assert.AreEqual(new Point(5, 1), paths[^1]);
        }
        
        private static SinglyLinkedList<Point> GetPaths(ArenaMap map, Point position, int range, bool entityBlockingPath = true)
        {
            return AStarPF.FindPathToPlayer(map, position, range, entityBlockingPath);
        }
    }
}