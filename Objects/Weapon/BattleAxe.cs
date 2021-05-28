namespace Cave_Adventure
{
    public class BattleAxe : Weapon
    {
        public BattleAxe() : base(GlobalConst.BattleAxeFactor, GlobalConst.BattleAxeRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }

        public override string ToString()
        {
            return "Боевой топор";
        }
    }
}
