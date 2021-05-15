using System.Timers;
using System.Drawing;

namespace Cave_Adventure
{
    public class Spider : Monster
    {
        private Timer _timer;

        public Spider(Point position) : base(position, EntityType.Spider)
        {
            AP = GlobalConst.SpiderAP;
            Attack = GlobalConst.SpiderAttack;
            Health = GlobalConst.SpiderHP;
            Damage = GlobalConst.SpiderDamage;
            Defense = GlobalConst.SpiderDefence;
        }

        public override double Attacking()
        {
            _timer = new Timer { Interval = 2000 };
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
            SetAnimation(StatesOfAnimation.Attack);
            return base.Attacking();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            SetAnimation(StatesOfAnimation.Idle);
            _timer.Stop();
            _timer = null;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.SpiderAP : 0;
        }

        public override string ToString()
        {
            return "Spider";
        }
    }
}