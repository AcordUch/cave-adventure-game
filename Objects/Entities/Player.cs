using System.Drawing;
using System.Timers;

namespace Cave_Adventure
{
    public class Player : Entity
    {
        Timer timer;

        public Player(Point position) : base(position, EntityType.Player)
        {
            AP = GlobalConst.PlayerAP;
            Attack = GlobalConst.PlayerAttack;
            Health = GlobalConst.PlayerHP;
            Damage = GlobalConst.PlayerDamage;
            Defense = GlobalConst.PlayerDefence;
            Weapon = new Sword();
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.PlayerAP : 0;
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

        public override string ToString()
        {
            return "Player";
        }
    }
}