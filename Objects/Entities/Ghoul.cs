﻿using System.Drawing;

namespace Cave_Adventure
{
    public class Ghoul : Monster
    {
        public Ghoul(Point position) : base(position, EntityType.Ghoul)
        {
            Weapon = new VampireSwing();
            AP = MaxAP = GlobalConst.GhoulAP;
            Attack = GlobalConst.GhoulAttack;
            Health = MaxHealth = GlobalConst.GhoulHP;
            Damage = GlobalConst.GhoulDamage;
            Defense = GlobalConst.GhoulDefence;
            Initiative = 9;
            Description = "Это существо прекрасно владеет руками и не только...";
            AI = new GhoulAI(this);
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.GhoulAP : 0;
        }

        public override string ToString()
        {
            return "Вурдалак";
        }
    }
}