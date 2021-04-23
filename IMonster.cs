using System.Drawing;

namespace Cave_Adventure
{
    public interface IMonster
    {
        Point Position { get; set; }
        void Move(int dx, int dy);
    }
}