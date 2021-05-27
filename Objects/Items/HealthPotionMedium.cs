using Cave_Adventure.Supporting.Interfaces;

namespace Cave_Adventure.Objects.Items
{
    public class HealthPotionMedium : Item, IHealthPotion
    {
        public int HealPower { get; } = 30;
        
        public HealthPotionMedium()
            :base()
        {
        }
    }
}