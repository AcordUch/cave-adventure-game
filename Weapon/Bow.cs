namespace Cave_Adventure
{
    public class Bow : AbstractWeapon
    {
        private const double BowFactor = 1; 
        public Bow() : base(GlobalConst.BowFactor, GlobalConst.BowRadius)
        {
        }
    }
}