using System.Drawing;
using System.Timers;

namespace Cave_Adventure
{
    public class Player : Entity
    {
        public Player(Point position) : base(position, EntityType.Player)
        {
            AP = MaxAP = GlobalConst.PlayerAP;
            Attack = GlobalConst.PlayerAttack;
            Health = MaxHealth = GlobalConst.PlayerHP;
            Damage = GlobalConst.PlayerDamage;
            Defense = GlobalConst.PlayerDefence;
            Initiative = 10;
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