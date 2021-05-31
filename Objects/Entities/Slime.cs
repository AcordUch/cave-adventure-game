using System.Drawing;

namespace Cave_Adventure
{
    public class Slime : Monster
    {
        public Slime(Point position) : base(position, EntityType.Slime)
        {
            Weapon = new StickyBody();
            AP = MaxAP = GlobalConst.SlimeAP;
            Attack = GlobalConst.SlimeAttack;
            Health = MaxHealth = GlobalConst.SlimeHP;
            Damage = GlobalConst.SlimeDamage;
            Defense = GlobalConst.SlimeDefence;
            Initiative = 5;
            Description = "Самый слабый, но не стоит его недооценивать";
            AI = new SlimeAI(this);
            DetectionRange = GlobalConst.SlimeDetectionRadius;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.SlimeAP : 0;
        }

        public override string ToString()
        {
            return "Слайм";
        }
    }
}
