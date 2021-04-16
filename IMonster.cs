using System.Drawing;

namespace Cave_Adventure
{
    public interface IMonster
    {
        Point Position { get; set; }
        void Move(); //Вообще, пусть пока будет так
    }
}