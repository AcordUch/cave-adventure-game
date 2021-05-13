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
        }

        public override void Attacking(in double playerHealth)
        {
            timer = new Timer { Interval = 2000 };
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
            SetAnimation(StatesOfAnimation.Attack);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            SetAnimation(StatesOfAnimation.Idle);
        }

        public override void ResetAP()
        {
            AP = GlobalConst.SnakeAP;
        }
    }
}