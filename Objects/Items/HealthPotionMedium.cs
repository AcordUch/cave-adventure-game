using Cave_Adventure.Supporting.Interfaces;

namespace Cave_Adventure.Objects.Items
{
    public class HealthPotionMedium : Item, IHealthPotion
    {
        public int HealPower { get; } = GlobalConst.MediumHealPower;
        
        public HealthPotionMedium()
            :base()
        {
        }
    }
}