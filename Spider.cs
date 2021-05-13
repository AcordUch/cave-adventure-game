using System.Timers;
using System.Drawing;

namespace Cave_Adventure
{
    public class Spider : Monster
    {
        Timer timer;

        public Spider(Point position) : base(position, EntityType.Spider)
        {
            AP = GlobalConst.SpiderAP;
            Attack = GlobalConst.SpiderAttack;
            Health = GlobalConst.SpiderHP;
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
            AP = GlobalConst.SpiderAP;
        }
    }
}