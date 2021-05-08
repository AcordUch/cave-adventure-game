using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cave_Adventure
{
    [TestFixture]
    public class BFSTests
    {
        [Test]
        public void BFSNoPath()
        {
            var textArena =
@"# .P .
# .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 3);
            //AssertPaths(paths, arena, new []{1});
            Assert.IsEmpty(paths);
        }
        
        [Test]
        public void BFSNoPath2()
        {
            var textArena =
@"# .P .
  .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 3);
            Assert.IsEmpty(paths);
        }
        
        [Test]
        public void BFSNoPathBecauseMonster()
        {
            var textArena =
@"# .P .
# .Sn.";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 3);
            Assert.IsEmpty(paths);
        }
        
        [Test]
        public void BFSCloseMonterAndWall()
        {
            var textArena =
@"# .P .
  .Sn.";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 3);
            // AssertPaths(paths, arena, new []{2});
            Assert.IsEmpty(paths);
        }
        
        [Test]
        public void BSFEmptyMapWithPlayer()
        {
            var textArena =
@"  .P .  .
  .  .  .
  .  .  .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 1);
            AssertPaths(paths, arena, new[]{2, 2, 2});
        }
        
        [Test]
        public void BSFSimpleArena()
        {
            var textArena =
@"P .  .  .
# .  .# .
# .  .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 2);
            AssertPaths(paths, arena, new[]{2, 3, 3});
        }
        
        [Test]
        public void BSFSimpleArenaWithMonster()
        {
            var textArena =
@"P .  .  .
# .Sn.# .
# .  .# .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 2);
            AssertPaths(paths, arena, new[]{2, 3});
        }
        
        [Test]
        public void BSFWallInCenter()
        {
            var textArena =
@"  .  .  .
P .# .  .
  .  .  .";
            var arena = ArenaMap.CreateNewArenaMap(textArena);
            var paths = GetPaths(arena, 3);
            AssertPaths(paths, arena, new[]{2, 2, 3, 3, 4, 4});
        }
        
        private static List<Point>[] GetPaths(ArenaMap map, int range)
        {
            return BFS.FindPaths(map, map.Player.Position, range)
                .Select(x => x.ToList())
                .ToArray();
        }
        
        private void AssertPaths(List<Point>[] paths, ArenaMap map, int[] expectedLengths)
        {
            Assert.AreEqual(expectedLengths.Length, paths.Length, $"Количество путей должно равняться {expectedLengths.Length}");
            for (var i = 0; i < paths.Length; i++)
            {
                AssertPath(map, paths[i], expectedLengths[i]);
            }
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