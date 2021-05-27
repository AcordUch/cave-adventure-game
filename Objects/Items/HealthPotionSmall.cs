using Cave_Adventure.Supporting.Interfaces;

namespace Cave_Adventure.Objects.Items
{
    public class HealthPotionSmall : Item, IHealthPotion
    {
        public int HealPower { get; } = 10;

        public HealthPotionSmall() : base()
        {
            
        }
    }
}