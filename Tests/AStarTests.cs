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
        
        private static SinglyLinkedList<Point> GetPaths(ArenaMap map, Point position, int range)
        {
            return AStarPF.FindPathToPlayer(map, position, range);
        }
        
        private void AssertPath(ArenaMap map, List<Point> path, int expectedLength)
        {
            var directions = GenerateDeltaPoint();
            Assert.IsNotEmpty(path, "Путь не должен быть пустым");
            for (var i = 0; i < path.Count - 1; i++)
            {
                Point offset = path[i + 1] - new Size(path[i]);
                Assert.IsTrue(directions.Contains(offset), $"Некорректный шаг #{i} в пути: с {path[i]} на {path[i + 1]}");
                Assert.AreNotEqual(CellType.Wall, map.Arena[path[i + 1].X, path[i + 1].Y], $"Коллизия со стеной на шаге {i}, точка: {path[i + 1]}");
            }
            Assert.AreEqual(map.Player.Position, path.Last(), "Последняя точка должна совпадать с позицией игрока");
        }
        
        private List<Point> GenerateDeltaPoint()
        {
            var result = new List<Point>();
            for (int y = -1; y < 2; y++)
            for (int x = -1; x < 2; x++)
            {
                if(x == 0 && y == 0) continue;
                result.Add(new Point(x, y));
            }

            return result;
        }
    }
}