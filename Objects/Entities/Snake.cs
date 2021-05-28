using System.Drawing;
using System.Timers;

namespace Cave_Adventure
{
    public class Snake: Monster
    {
        public Snake(Point position) : base(position, EntityType.Snake)
        {
            AP = MaxAP = GlobalConst.SnakeAP;
            Attack = GlobalConst.SnakeAttack;
            Health = MaxHealth = GlobalConst.SnakeHP;
            Damage = GlobalConst.SnakeDamage;
            Defense = GlobalConst.SnakeDefence;
            Initiative = 7;
            Description = "Она не ядовитая, честно";
            AI = new SnakeAI(this);
            DetectionRange = GlobalConst.SnakeDetectionRadius;
        }

        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.SnakeAP : 0;
        }

        public override string ToString()
        {
            return "Змея";
        }
    }
}