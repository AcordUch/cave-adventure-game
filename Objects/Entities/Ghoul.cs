using System.Drawing;

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
            Description = "Острые клыки, стальные когти - это всё про него";
            AI = new GhoulAI(this);
            DetectionRange = GlobalConst.GhoulDetectionRadius;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.GhoulAP : 0;
        }

        public override string ToString()
        {
            return "Гуль";
        }
    }
}