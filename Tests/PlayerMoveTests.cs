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
            "  .  .  .",
            "  .P .  .",
            "  .  .  ."
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
        public void MoveUp()
        {
            
        }
    }
}