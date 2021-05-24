namespace Cave_Adventure
{
    public class FangsAndClaws : Weapon
    {
        public FangsAndClaws() : base(GlobalConst.FangsAndClawsFactor, GlobalConst.FangsAndClawsRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }
        
        public override string ToString()
        {
            return "Когти, зубы, лапы";
        }
    }
}