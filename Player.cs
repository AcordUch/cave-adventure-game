using System.Drawing;

namespace Cave_Adventure
{
    public class Player
    {
        public Point Position { get; set; }
        
        public Player(Point position)
        {
            Position = position;
        }

        public Player(Player player)
            : this(player.Position)
        {}
    }
}