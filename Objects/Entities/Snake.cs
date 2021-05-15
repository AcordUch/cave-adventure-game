using System.Drawing;
using System.Timers;

namespace Cave_Adventure
{
    public class Snake: Monster
    {
        private Timer _timer;

        public Snake(Point position) : base(position, EntityType.Snake)
        {
            AP = GlobalConst.SnakeAP;
            Attack = GlobalConst.SnakeAttack;
            Health = GlobalConst.SnakeHP;
            Damage = GlobalConst.SnakeDamage;
            Defense = GlobalConst.SnakeDefence;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.SnakeAP : 0;
        }

        public override string ToString()
        {
            return "Snake";
        }
    }
}