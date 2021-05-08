using System.Drawing;

namespace Cave_Adventure
{
    public class Spider : Monster
    {
        public Spider(Point position) : base(position, EntityType.Spider)
        {
        }

        public override void Attacking()
        {
            base.Attacking();
        }

        public override void ResetAP()
        {
            AP = GlobalConst.SpiderAP;
        }
    }
}