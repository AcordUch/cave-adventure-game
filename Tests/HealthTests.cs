using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Cave_Adventure.Objects.Items;
using Cave_Adventure.Supporting.Interfaces;
using NUnit.Framework;

namespace Cave_Adventure
{
    [TestFixture]
    public class HealthAndHealthBagTests
    {
        private Player _player;
        private ArenaMap _arena;
        private Spider _spider;

        [SetUp]
        public void OnLoad()
        {
            var arena = new string[]
            {
                "P .  .",
                "Sp.  .",
            };
            
            _arena = ArenaMap.CreateNewArenaMap(arena);
            _player = _arena.Player;
            _spider = _arena.Monsters[0] as Spider;
        }

        private void AddPotion()
        {
            for (int _ = 0; _ < 3; _++)
            {
                _player.Inventory.AddHeals(new HealthPotionMedium());
            }
        }

        [Test]
        public void PotionAdd()
        {
            for (int i = 1; i <= 3; i++)
            {
                _player.Inventory.AddHeals(new HealthPotionMedium());
                Assert.AreEqual(i, _player.Inventory.AmountOfPotion);
            }
        }
        
        [Test]
        public void PotionRemovesAfterUsing()
        {
            AddPotion();
            
            for (int i = 1; i <= 3; i++)
            {
                _player.Inventory.TryGetHealthPotion(out var healKit);
                Assert.AreEqual(3 - i, _player.Inventory.AmountOfPotion);
            }
        }

        [Test]
        public void TestHealthPotion()
        {
            _player.Inventory.AddHeals(new HealthPotionSmall());
            _player.Inventory.AddHeals(new HealthPotionMedium());
            _player.Inventory.AddHeals(new HealthPotionBig());

            _player.Health = 1;
            _player.Inventory.TryGetSmallHealthPotion(out var healKitSmall);
            _player.Health += healKitSmall.HealPower;
            Assert.AreEqual(healKitSmall.HealPower + 1, _player.Health);

            _player.Health = 1;
            _player.Inventory.TryGetMediumHealthPotion(out var healKitMedium);
            _player.Health += healKitMedium.HealPower;
            Assert.AreEqual(healKitMedium.HealPower + 1, _player.Health);
            
            _player.Health = 1;
            _player.Inventory.TryGetBigHealthPotion(out var healKitBig);
            _player.Health += healKitBig.HealPower;
            Assert.AreEqual(healKitBig.HealPower + 1, _player.Health);
        }

        [Test]
        public void TestMonsterDrop()
        {
            for (int i = 0; i < 500; i++)
            {
                _arena.AddHeal();
                Thread.Sleep(1);
            }
            
            Assert.IsTrue(_player.Inventory.AmountOfPotion > 0);
            Assert.IsTrue(_player.Inventory.TryGetSmallHealthPotion(out var ps));
            Assert.IsTrue(_player.Inventory.TryGetMediumHealthPotion(out var pm));
            Assert.IsTrue(_player.Inventory.TryGetBigHealthPotion(out var pb));
        }
    }
}