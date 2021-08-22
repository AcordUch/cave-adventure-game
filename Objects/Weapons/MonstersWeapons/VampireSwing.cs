namespace Cave_Adventure
{
    public class VampireSwing : Weapon
    {
        public VampireSwing() : base(GlobalConst.VampireSwingFactor, GlobalConst.VampireSwingRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }

        public override string ToString()
        {
            return "Взмах Вампира";
        }
    }
}
