using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using Cave_Adventure.Objects;
using Cave_Adventure.Objects.Items;

namespace Cave_Adventure
{
    public class Player : Entity
    {
        // public List<Item> Inventory { get; } = new List<Item>();
        public PlayerInventory Inventory { get; } = new PlayerInventory();
        
        public Player(Point position) : base(position, EntityType.Player)
        {
            AP = MaxAP = GlobalConst.PlayerAP;
            Attack = GlobalConst.PlayerAttack;
            Health = MaxHealth = GlobalConst.PlayerHP;
            Damage = GlobalConst.PlayerDamage;
            Defense = GlobalConst.PlayerDefence;
            Initiative = 10;
            Weapon = new Sword();
            Description = "Как он оказался в пещере - загадка века";
        }

        public void UseHealthPotionFromInventory()
        {
            var healKit = Inventory.HealthPotionsBag.FirstOrDefault();
            if(healKit == null) return;
            Health += healKit.HealPower;
            Inventory.RemoveHeal(healKit);
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.PlayerAP : 0;
        }

        public override string ToString()
        {
            return "Воин";
        }
    }
}