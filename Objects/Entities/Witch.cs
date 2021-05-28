using System.Drawing;

namespace Cave_Adventure
{
    public class Witch : Monster
    {
        public Witch(Point position) : base(position, EntityType.Witch)
        {
            Weapon = new Spell();
            AP = MaxAP = GlobalConst.WitchAP;
            Attack = GlobalConst.WitchAttack;
            Health = MaxHealth = GlobalConst.WitchHP;
            Damage = GlobalConst.WitchDamage;
            Defense = GlobalConst.WitchDefence;
            Initiative = 10;
            Description = "От её заклинаний ещё никто не оставался в живых...";
            AI = new WitchAI(this);
            DetectionRange = GlobalConst.WitchDetectionRadius;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.WitchAP : 0;
        }

        public override string ToString()
        {
            return "Ведьма";
        }
    }
}
