using System.Drawing;

namespace Cave_Adventure
{
    public class Spider : Monster
    {
        public Spider(Point position, EntityType tag) : base(position, tag)
        {
        }

        public override void Attacking()
        {
            base.Attacking();
        }
    }
}