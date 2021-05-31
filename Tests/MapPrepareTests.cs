using System;
using NUnit.Framework;

namespace Cave_Adventure
{
    [TestFixture]
    public class MapPrepareTests
    {
        [Test]
        public void NoNeedPrepare()
        {
            var arena = new[]
            {
                "#1.#2.",
                "#1.P .",
                "#1.#1.",
            };
            var maxRow = 3;
            var maxColumn = 2;
            var expected =
                $@"#1.#2.
#1.P .
#1.#1.
";
            var result = ArenaParser.PrepareMap(arena, maxRow, maxColumn);
            Console.WriteLine(result);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void SimplePrepare()
        {
            var arena = new[]
            {
                "#1.#2.#2.",
                "#1.P .#3.",
                "#1.#1.#1.",
            };
            var maxRow = 5;
            var maxColumn = 7;
            var expected =
$@"#T.#T.#T.#T.#T.#T.#T.
#T.#T.#1.#2.#2.#T.#T.
#T.#T.#1.P .#3.#T.#T.
#T.#T.#1.#1.#1.#T.#T.
#T.#T.#T.#T.#T.#T.#T.
";
            var result = ArenaParser.PrepareMap(arena, maxRow, maxColumn);
            Console.WriteLine(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SimplePrepare2()
        {
            var arena = new[]
            {
                "#1.#2.#2.",
                "#1.P .#3.",
            };
            var maxRow = 5;
            var maxColumn = 7;
            var expected =
$@"#T.#T.#T.#T.#T.#T.#T.
#T.#T.#T.#T.#T.#T.#T.
#T.#T.#1.#2.#2.#T.#T.
#T.#T.#1.P .#3.#T.#T.
#T.#T.#T.#T.#T.#T.#T.
#T.#T.#T.#T.#T.#T.#T.
";
            var result = ArenaParser.PrepareMap(arena, maxRow, maxColumn);
            Console.WriteLine(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SimplePrepare3()
        {
            var arena = new[]
            {
                "#1.#2.",
                "#1.P .",
                "#1.#1.",
            };
            var maxRow = 3;
            var maxColumn = 5;
            var expected =
$@"#T.#T.#1.#2.#T.#T.
#T.#T.#1.P .#T.#T.
#T.#T.#1.#1.#T.#T.
";
            var result = ArenaParser.PrepareMap(arena, maxRow, maxColumn);
            Console.WriteLine(result);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void SimplePrepare4()
        {
            var arena = new[]
            {
                "#1.#2.",
                "#1.P .",
                "#1.#1.",
            };
            var maxRow = 3;
            var maxColumn = 3;
            var expected =
$@"#T.#1.#2.#T.
#T.#1.P .#T.
#T.#1.#1.#T.
";
            var result = ArenaParser.PrepareMap(arena, maxRow, maxColumn);
            Console.WriteLine(result);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void MapBiggerThanLimit()
        {
            var arena = new[]
            {
                "#1.#2.#2.",
                "#1.P .#3.",
                "#1.#1.#1.",
            };
            var maxRow = 1;
            var maxColumn = 1;
            var expected =
$@"#1.#2.#2.
#1.P .#3.
#1.#1.#1.
";
            var result = ArenaParser.PrepareMap(arena, maxRow, maxColumn);
            Console.WriteLine(result);
            Assert.AreEqual(expected, result);
        }
    }
}