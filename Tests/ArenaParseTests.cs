using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;

namespace Cave_Adventure
{
    [TestFixture]
    public class ArenaSpliteTest
    {
        [Test]
        public void SpliteEmptyArena()
        {
            var TextArena = new string[0];
            Assert.AreEqual(new string [0,0], ArenaParser.PublicGetterForTestsDaYaDurakChtoTakDelau(TextArena));
        }
        
        [Test]
        public void SpliteSimpleArena()
        {
            var TextArena = new[]
            {
                "P .  .  .",
                "  .  .  .",
                "  .  .Sn."
            };
            var expected = new[,]
            {
                {"P ", "  ", "  "},
                {"  ", "  ", "  "},
                {"  ", "  ", "Sn"},
            };
            AssertsArena(ArenaParser.PublicGetterForTestsDaYaDurakChtoTakDelau(TextArena), expected);
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
            AssertsArena(ArenaParser.PublicGetterForTestsDaYaDurakChtoTakDelau(TextArena), expected);
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

            AssertsArena(ArenaParser.ParsingMap(textArena), expectedArena,
                new Point[0], new Point(1, 1));
        }
        
        
        [Test]
        public void ParseEmptyArena()
        {
            var textArena = new string[0];
            AssertsArena(ArenaParser.ParsingMap(textArena), new CellType[0,0],
                new Point[0], new Point().NegativePoint());
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
            AssertsArena(ArenaParser.ParsingMap(textArena), expectedArena,
                new Point[0], new Point().NegativePoint());
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
            AssertsArena(ArenaParser.ParsingMap(textArena), expectedArena,
                new Point[0], new Point(1, 0));
        }
        
        [Test]
        public void ParseArenaWithPlayerAndMonsters()
        {
            var textArena = new[]
            {
                "  .P .  .",
                "  .  .  .",
                "Sn.Sp.Sn."
            };
            var actualArena = ArenaParser.ParsingMap(textArena);
            var actualMonstersType = actualArena.monsters.Select(m => m.Tag).ToArray();
            
            var expectedArena = new[,]
            {
                {CellType.Floor, CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor, CellType.Floor},
                {CellType.Floor, CellType.Floor, CellType.Floor}
            };
            var expectedMonstersPosition = new[]
            {
                new Point(0, 2), new Point(1, 2), new Point(2, 2)
            };
            var expectedMonstersType = new[]
            {
                MonsterType.Snake, MonsterType.Spider, MonsterType.Snake
            };
                
            AssertsArena(actualArena, expectedArena,
                expectedMonstersPosition, new Point(1, 0));
            AssertMonstersType(actualMonstersType, expectedMonstersType);
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

            AssertsArena(ArenaParser.ParsingMap(textArena), expectedArena,
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

            AssertsArena(ArenaParser.ParsingMap(textArena), expectedArena,
                expectedMonsters, new Point(0, 1));
        }
        
        [Test]
        public void ParseSimpleArena2()
        {
            var textArena = new[]
            {
                "# .P .# .# .",
                "# .  .  .  .",
                "# .Sn.  .Sn.",
                "  .  .  .  ."
            };
            var actualArena = ArenaParser.ParsingMap(textArena);
            var actualMonstersType = actualArena.monsters.Select(m => m.Tag).ToArray();
            
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
            var expectedMonstersType = new[]
            {
                MonsterType.Snake, MonsterType.Snake
            };
                
            AssertsArena(actualArena, expectedArena,
                expectedMonsters, new Point(1, 0));
            AssertMonstersType(actualMonstersType, expectedMonstersType);
        }
        
        [Test]
        public void ParseArenaFromText()
        {
            var textArena =
@"# .P .  .
# .  .  .
# .# .Sp.";
            var actualArena = ArenaParser.ParsingMap(textArena);
            var actualMonstersType = actualArena.monsters.Select(m => m.Tag).ToArray();

            var expectedArena = new[,]
            {
                {CellType.Wall, CellType.Floor, CellType.Floor},
                {CellType.Wall, CellType.Floor, CellType.Floor},
                {CellType.Wall, CellType.Wall, CellType.Floor}
            };
            var expectedMonstersType = new[]
            {
                MonsterType.Spider
            };
            
            AssertsArena(actualArena, expectedArena,
                new []{new Point(2, 2)}, new Point(1, 0));
            AssertMonstersType(actualMonstersType, expectedMonstersType);
        }

        private static void AssertsArena((CellType[,] arenaMap, Player player, Monster[] monsters) arena,
            CellType[,] expectedArena, Point[] expextedMonsters, Point player)
        {
            Assert.AreEqual(expectedArena.Length, arena.arenaMap.Length, "Размеры не совпадают с ожидаемыми");
            Assert.AreEqual(expextedMonsters, arena.monsters.Select(m => m.Position), "Расположение монстров не совпадает с ожидаемым");
            Assert.AreEqual(player, arena.player.Position, "Расположение игрока не совпадает с ожидаемым");
            for (int y = 0; y < expectedArena.GetLength(0); y++)
            for (int x = 0; x < expectedArena.GetLength(1); x++)
            {
                Assert.AreEqual(expectedArena[y, x], arena.arenaMap[x, y], $"Ошибка на клетке X: {x}, Y: {y}");
            }
            //Примечание: На выходе мы как бы получаем транспонированную матрицу
            //То есть: Раньше по первому измерению хранились строки(y), после хранятся стобцы(x)
        }

        private static void AssertMonstersType(MonsterType[] actualMonsters, MonsterType[] expextedMonsters)
        {
            Assert.AreEqual(expextedMonsters.Length, actualMonsters.Length, "Количество монстров не совпадает с ожидаемым");
            Assert.AreEqual(expextedMonsters, actualMonsters, "Содержимое массивов монстров разное");
        }
    }
}