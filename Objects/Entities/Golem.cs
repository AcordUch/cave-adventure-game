using System.Drawing;

namespace Cave_Adventure
{
    public class Golem : Monster
    {
        public Golem(Point position) : base(position, EntityType.Golem)
        {
            Weapon = new StonePaws();
            AP = MaxAP = GlobalConst.GolemAP;
            Attack = GlobalConst.GolemAttack;
            Health = MaxHealth = GlobalConst.GolemHP;
            Damage = GlobalConst.GolemDamage;
            Defense = GlobalConst.GolemDefence;
            Initiative = 8;
            Description = "Его кулаки - это грозное оружие";
            AI = new GolemAI(this);
            DetectionRange = GlobalConst.GolemDetectionRadius;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.GolemAP : 0;
        }

        public override string ToString()
        {
            return "Голем";
        }
    }
}
