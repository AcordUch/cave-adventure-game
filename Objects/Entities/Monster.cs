using System.Drawing;

namespace Cave_Adventure
{
    public class Monster: Entity
    {
        public AI AI { get; protected init; }
        
        protected Monster(Point position, EntityType tag) : base(position, tag)
        {
            Weapon = new Fangs();
            // AI = new AI(this);
            Description = "Злобное существо из подземелий";
        }
    }
}