using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cave_Adventure
{
    [TestFixture]
    public class PlayerMoveTests
    {
        private static readonly string[] SimpleEmptyArena = new []
        {
            "P .  .",
            "  .  .",
        };
        
        private static readonly string[] EmptyArenaWithColumn = new []
        {
            "  .  .  .",
            "P .W .  .",
            "  .  .  ."
        };
        
        private static readonly string[] ArenaWithMonster = new []
        {
            "# .# .# .",
            "P .M .# .",
            "  .  .  ."
        };
        
        [Test]
        public void SimpleMoveWithOutMap()
        {
            var player = new Player(Point.Empty);
            player.Move(1, 0);
            Assert.AreEqual(new Point(1, 0), player.Position);
            player.Move(0, 1);
            Assert.AreEqual(new Point(1, 1), player.Position);
            player.Move(-1, 0);
            Assert.AreEqual(new Point(0, 1), player.Position);
            player.Move(0, -1);
            Assert.AreEqual(new Point(0, 0), player.Position);
        }

        [Test]
        public void SimpleMoveOnEmptyMap()
        {
            var arena = ArenaMap.CreateNewArenaMap(SimpleEmptyArena);
            arena.Player.Move(1, 0);
            Assert.AreEqual(new Point(1, 0),  arena.Player.Position);
            arena.Player.Move(0, 1);
            Assert.AreEqual(new Point(1, 1),  arena.Player.Position);
            arena.Player.Move(-1, 0);
            Assert.AreEqual(new Point(0, 1),  arena.Player.Position);
            arena.Player.Move(0, -1);
            Assert.AreEqual(new Point(0, 0),  arena.Player.Position);
        }
    }
}