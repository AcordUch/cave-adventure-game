using NUnit.Framework;
using System;
using System.Drawing;

namespace Cave_Adventure
{
    [TestFixture]
    public class ArenaSpliteTest
    {
        [Test]
        public void SpliteEmptyArena()
        {
            var TextArena = new string[0];
            Assert.AreEqual(new string [0,0], ArenaMap.PublicGetterForTestsDaYaDurakChtoTakDelau(TextArena));
        }
        
        [Test]
        public void SpliteSimpleArena()
        {
            var TextArena = new[]
            {
                "P .  .  .",
                "  .  .  .",
                "  .  .M ."
            };
            var expected = new[,]
            {
                {"P ", "  ", "  "},
                {"  ", "  ", "  "},
                {"  ", "  ", "M "},
            };
            AssertsArena(ArenaMap.PublicGetterForTestsDaYaDurakChtoTakDelau(TextArena), expected);
        }

        [Test]
        public void SpliteArenaWithWall()
        {
            var TextArena = new[]
            {
                "# .  .  .",
                "# .  .  .",
                "# .  .  ."
            };
            var expected = new[,]
            {
                {"# ", "  ", "  "},
                {"# ", "  ", "  "},
                {"# ", "  ", "  "}
            };
            AssertsArena(ArenaMap.PublicGetterForTestsDaYaDurakChtoTakDelau(TextArena), expected);
        }
        
        private static void AssertsArena(string[,] arena, string[,] expectedArena)
        {
            for (int y = 0; y < expectedArena.GetLength(0); y++)
            for (int x = 0; x < expectedArena.GetLength(1); x++)
            {
                Assert.AreEqual(expectedArena[x, y], arena[x, y], $"Ошибка на клетке X: {x}, Y: {y}");
            }
        }
    }

    [TestFixture]
    public class ArenaParseTests
    {
        [Test]
        public void ParseSimpleEmptyArenaFromReadyCell()
        {
            var textArena = new[,]
            {
                {"# ", "  "},
                {"# ", "P "}
            };
            
            var expectedArena = new[,]
            {
                {CellType.Wall, CellType.Floor},
                {CellType.Wall, CellType.Floor},
            };

            AssertsArena(ArenaMap.ParsingMap(textArena), expectedArena,
                new Point[0], new Point(1, 1));
        }
        
        
        [Test]
        public void ParseEmptyArena()
        {
            var textArena = new string[0];
            AssertsArena(ArenaMap.ParsingMap(textArena), new CellType[0,0],
                new Point[0], Point.Empty);
        }

        [Test]
        public void ParseOnlyFloorArena()
        {
            var textArena = new[]
            {
                "  .  .",
                "  .  ."
            };
            var expectedArena = new[,]
            {
                {CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor}
            };
            AssertsArena(ArenaMap.ParsingMap(textArena), expectedArena,
                new Point[0], Point.Empty);
        }

        [Test]
        public void ParseArenaWithPlayer()
        {
            var textArena = new[]
            {
                "  .P .",
                "  .  .",
                "  .  ."
            };
            var expectedArena = new[,]
            {
                {CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor}
            };
            AssertsArena(ArenaMap.ParsingMap(textArena), expectedArena,
                new Point[0], new Point(1, 0));
        }
        
        [Test]
        public void ParseArenaWithPlayerAndMonsters()
        {
            var textArena = new[]
            {
                "  .P .  .",
                "  .  .  .",
                "M .M .M ."
            };
            
            var expectedArena = new[,]
            {
                {CellType.Floor, CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor, CellType.Floor}
            };
            var expectedMonsters = new[]
            {
                new Point(0, 2), new Point(1, 2), new Point(2, 2)
            };
                
            AssertsArena(ArenaMap.ParsingMap(textArena), expectedArena,
                expectedMonsters, new Point(1, 0));
        }
        
        [Test]
        public void ParseSimpleEmptyArena1()
        {
            var textArena = new[]
            {
                "# .  .",
                "# .P ."
            };
            
            var expectedArena = new[,]
            {
                {CellType.Wall, CellType.Floor},
                {CellType.Wall, CellType.Floor},
            };

            AssertsArena(ArenaMap.ParsingMap(textArena), expectedArena,
                new Point[0], new Point(1, 1));
        }
        
        [Test]
        public void ParseSimpleArena1()
        {
            var textArena = new[]
            {
                "# .  .# .",
                "P .  .# .",
                "# .# .# ."
            };
            
            var expectedArena = new[,]
            {
                {CellType.Wall, CellType.Floor, CellType.Wall},
                {CellType.Floor, CellType.Floor, CellType.Wall},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            };
            var expectedMonsters = new Point[0];

            AssertsArena(ArenaMap.ParsingMap(textArena), expectedArena,
                expectedMonsters, new Point(0, 1));
        }
        
        [Test]
        public void ParseSimpleArena2()
        {
            var textArena = new[]
            {
                "# .P .# .# .",
                "# .  .  .  .",
                "# .M .  .M .",
                "  .  .  .  ."
            };
            
            var expectedArena = new[,]
            {
                {CellType.Wall, CellType.Floor, CellType.Wall, CellType.Wall},
                {CellType.Wall, CellType.Floor, CellType.Floor, CellType.Floor},
                {CellType.Wall, CellType.Floor, CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor, CellType.Floor, CellType.Floor}
            };
            var expectedMonsters = new[]
            {
                new Point(1, 2), new Point(3, 2)
            };
                
            AssertsArena(ArenaMap.ParsingMap(textArena), expectedArena,
                expectedMonsters, new Point(1, 0));
        }

        private static void AssertsArena(ArenaMap arena, CellType[,] expectedArena, Point[] expextedMonsters, Point player)
        {
            Assert.AreEqual(expectedArena.Length, arena.Arena.Length, "Размеры не совпадают с ожидаемыми");
            Assert.AreEqual(expextedMonsters, arena.Monsters, "Расположение монстров не совпадает с ожидаемым");
            Assert.AreEqual(player, arena.Player, "Расположение игрока не совпадает с ожидаемым");
            for (int y = 0; y < expectedArena.GetLength(0); y++)
            for (int x = 0; x < expectedArena.GetLength(1); x++)
            {
                Assert.AreEqual(expectedArena[y, x], arena.Arena[x, y], $"Ошибка на клетке X: {x}, Y: {y}");
            }
            //Примечание: На выходе мы как бы получаем транспонированную матрицу
            //То есть: Раньше по первому измерению хранились строки(y), после хранятся стобцы(x)
        }
    }
}