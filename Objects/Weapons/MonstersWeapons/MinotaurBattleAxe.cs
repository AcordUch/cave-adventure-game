namespace Cave_Adventure
{
    public class MinotaurBattleAxe : Weapon
    {
        public MinotaurBattleAxe() : base(GlobalConst.BattleAxeFactor, GlobalConst.BattleAxeRadius)
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
