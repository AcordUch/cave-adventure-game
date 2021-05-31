using System.Timers;
using System.Drawing;

namespace Cave_Adventure
{
    public class Spider : Monster
    {
        public Spider(Point position) : base(position, EntityType.Spider)
        {
            AP = MaxAP = GlobalConst.SpiderAP;
            Attack = GlobalConst.SpiderAttack;
            Health = MaxHealth = GlobalConst.SpiderHP;
            Damage = GlobalConst.SpiderDamage;
            Defense = GlobalConst.SpiderDefence;
            Initiative = 6;
            Description = "Хоть и маленький, но смелый";
            AI = new SpiderAI(this);
            DetectionRange = GlobalConst.SpiderDetectionRadius;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.SpiderAP : 0;
        }

        public override string ToString()
        {
            return "Паук Гордый";
        }
    }
}