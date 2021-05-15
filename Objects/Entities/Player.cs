using System.Drawing;
using System.Timers;

namespace Cave_Adventure
{
    public class Player : Entity
    {
        private Timer _timer;

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

        public override string ToString()
        {
            return "Player";
        }
    }
}