namespace Cave_Adventure
{
    public class Sword : Weapon
    {
        public Sword() : base(GlobalConst.SwordFactor, GlobalConst.SwordRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }
        
        public override string ToString()
        {
            return "Меч";
        }
    }
}