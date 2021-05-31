using Cave_Adventure.Supporting.Interfaces;

namespace Cave_Adventure.Objects.Items
{
    public class HealthPotionBig : Item, IHealthPotion
    {
        public int HealPower { get; } = GlobalConst.BigHealPower;
        
        public HealthPotionBig() : base()
        {
        }
    }
}