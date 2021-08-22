namespace Cave_Adventure
{
    public class WitchSpell : Weapon
    {
        public WitchSpell() : base(GlobalConst.SpellFactor, GlobalConst.SpellRadius)
        {
        }

        public override double GetDamage(in Entity entity)
        {
            entity.ReduceAP(entity.AP);
            return base.GetDamage(in entity);
        }

        public override string ToString()
        {
            return "Заклинание";
        }
    }
}
