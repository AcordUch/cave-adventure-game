using System.Drawing;

namespace Cave_Adventure
{
    public class Snake: Monster
    {
        public Snake(Point position) : base(position, EntityType.Snake)
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