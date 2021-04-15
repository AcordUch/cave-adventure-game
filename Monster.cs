using System.Drawing;

namespace Cave_Adventure
{
    public class Monster: IMonster
    {
        public Point Position { get; set; }

        public Monster(Point position)
        {
            Position = position;
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}