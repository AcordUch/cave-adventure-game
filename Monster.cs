using System.Drawing;

namespace Cave_Adventure
{
    public abstract class Monster: Entity
    {
        protected Monster(Point position, EntityType tag) : base(position, tag)
        {
        }
    }
}