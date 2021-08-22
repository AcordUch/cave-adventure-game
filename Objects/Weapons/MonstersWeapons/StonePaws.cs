namespace Cave_Adventure
{
    public class StonePaws : Weapon
    {
        public StonePaws() : base(GlobalConst.StonePawsFactor, GlobalConst.StonePawsRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }

        public override string ToString()
        {
            return "Каменные лапы";
        }
    }
}
