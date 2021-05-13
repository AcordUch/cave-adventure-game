using System.Drawing;

namespace Cave_Adventure
{
    public class Monster: Entity
    {
        protected Monster(Point position, EntityType tag) : base(position, tag)
        {
            Weapon = new FangsAndClaws();
        }
    }
}