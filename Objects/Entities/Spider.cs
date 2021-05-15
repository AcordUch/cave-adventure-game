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
            Damage = GlobalConst.SpiderDamage;
            Defense = GlobalConst.SpiderDefence;
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
            AP = Health > 0 ? GlobalConst.SpiderAP : 0;
        }

        public override string ToString()
        {
            return "Spider";
        }
    }
}