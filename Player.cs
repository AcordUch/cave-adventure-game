using System.Drawing;

namespace Cave_Adventure
{
    public class Player
    {
        private Point _position;

        public int X
        {
            get => _position.X;
            set => _position.X = value;
        }

        public int Y
        {
            get => _position.Y;
            set => _position.Y = value;
        }

        public Player(Point position)
        {
            _position = position;
        }

        public Player(Player player)
            : this(player._position)
        {}
    }
}