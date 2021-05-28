using System.Drawing;

namespace Cave_Adventure
{
    public class Minotaur : Monster
    {
        public Minotaur(Point position) : base(position, EntityType.Minotaur)
        {
            Weapon = new BattleAxe();
            AP = MaxAP = GlobalConst.MinotaurAP;
            Attack = GlobalConst.MinotaurAttack;
            Health = MaxHealth = GlobalConst.MinotaurHP;
            Damage = GlobalConst.MinotaurDamage;
            Defense = GlobalConst.MinotaurDefence;
            Initiative = 11;
            Description = "Грозный страж подземелья";
            AI = new MinotaurAI(this);
        }
        
        public override void ResetAP()
        {
            AP = Health > 0 ? GlobalConst.MinotaurAP : 0;
        }

        public override string ToString()
        {
            return "Минотавр";
        }
    }
}