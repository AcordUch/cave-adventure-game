using System.Drawing;

namespace Cave_Adventure
{
    public class Monster: Entity
    {
        public AI AI { get; }
        
        protected Monster(Point position, EntityType tag) : base(position, tag)
        {
            Weapon = new FangsAndClaws();
            AI = new AI(this);
            Description = "Злобное существо из подземелий";
        }
    }
}