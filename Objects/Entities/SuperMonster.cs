using System.Drawing;

namespace Cave_Adventure
{
    public class SuperMonster : Monster
    {
        public SuperMonster(Point position) : base(position, EntityType.SuperMonster)
        {
            AP = MaxAP = 999;
            Attack = 999;
            Health = MaxHealth = 999;
            Damage = 999;
            Defense = 999;
            Initiative = 999;
        }
        
        public override void ResetAP()
        {
            AP = Health > 0 ? 999 : 0;
        }

        public override string ToString()
        {
            return "Necesse mori";
        }
    }
}