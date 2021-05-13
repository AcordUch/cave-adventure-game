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
        }

        public override void ResetAP()
        {
            AP = GlobalConst.PlayerAP;
        }

        public override void Attacking(in double monsterHealth)
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
    }
}