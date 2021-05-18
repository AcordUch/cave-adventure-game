using System.Timers;
using System.Drawing;

namespace Cave_Adventure
{
    public class Spider : Monster
    {
        public Spider(Point position) : base(position, EntityType.Spider)
        {
            AP = GlobalConst.SpiderAP;
            Attack = GlobalConst.SpiderAttack;
            Health = GlobalConst.SpiderHP;
            Damage = GlobalConst.SpiderDamage;
            Defense = GlobalConst.SpiderDefence;
            Initiative = 6;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.SpiderAP : 0;
        }

        public override string ToString()
        {
            return "Spider";
        }
    }
}