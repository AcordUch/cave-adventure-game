namespace Cave_Adventure
{
    public class Bow : Weapon
    {
        public Bow() : base(GlobalConst.BowFactor, GlobalConst.BowRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP - 1);
            return base.GetDamage(in entity);
        }

        public override string ToString()
        {
            return "Лук";
        }
    }
}