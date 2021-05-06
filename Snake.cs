using System.Drawing;

namespace Cave_Adventure
{
    public class Snake: Monster
    {
        public Snake(Point position, EntityType tag) : base(position, tag)
        {
        }

        public override void Attacking()
        {
            base.Attacking();
        }

        public override void ResetAP()
        {
            AP = GlobalConst.SnakeAP;
        }
    }
}