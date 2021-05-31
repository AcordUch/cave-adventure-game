namespace Cave_Adventure
{
    public class StickyBody : Weapon
    {
        public StickyBody() : base(GlobalConst.StickyBodyFactor, GlobalConst.StickyBodyRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }

        public override string ToString()
        {
            return "Липкое тело";
        }
    }
}
