using System.Drawing;
using System.Timers;

namespace Cave_Adventure
{
    public class Snake: Monster
    {
        Timer timer;

        public Snake(Point position) : base(position, EntityType.Snake)
        {
            AP = GlobalConst.SnakeAP;
            Attack = GlobalConst.SnakeAttack;
            Health = GlobalConst.SnakeHP;
            Damage = GlobalConst.SnakeDamage;
            Defense = GlobalConst.SnakeDefence;
        }

        public override double Attacking()
        {
            timer = new Timer { Interval = 2000 };
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
            SetAnimation(StatesOfAnimation.Attack);
            return base.Attacking();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            SetAnimation(StatesOfAnimation.Idle);
        }

        public override void ResetAP()
        {
            AP = GlobalConst.SnakeAP;
        }

        public override string ToString()
        {
            return "Snake";
        }
    }
}