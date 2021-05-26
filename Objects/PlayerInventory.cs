using System.Collections.Generic;
using System.Linq;
using Cave_Adventure.Objects.Items;
using Cave_Adventure.Supporting.Interfaces;

namespace Cave_Adventure.Objects
{
    public class PlayerInventory
    {
        private readonly List<IHealthPotion> _healthPotionsBag = new List<IHealthPotion>();

        public IReadOnlyList<IHealthPotion> HealthPotionsBag => _healthPotionsBag;
        public int AmountOfPotion => _healthPotionsBag.Count;

        public void AddHeals(IHealthPotion healthPotion)
        {
            _healthPotionsBag.Add(healthPotion);
        }

        public void RemoveHeal(IHealthPotion healthPotion)
        {
            _healthPotionsBag.Remove(healthPotion);
        }

        public bool TryGetHealthPotion(out IHealthPotion healthPotion)
        {
            healthPotion = _healthPotionsBag.FirstOrDefault();
            _healthPotionsBag.Remove(healthPotion);
            return healthPotion != null;
        }

        public bool TryGetSmallHealthPotion(out HealthPotionSmall healthPotionSmall)
        {
            healthPotionSmall = _healthPotionsBag.FirstOrDefault(p => p is HealthPotionSmall) as HealthPotionSmall;
            _healthPotionsBag.Remove(healthPotionSmall);
            return healthPotionSmall != null;
        }
        
        public bool TryGetMediumHealthPotion(out HealthPotionMedium healthPotionMedium)
        {
            healthPotionMedium = _healthPotionsBag.FirstOrDefault(p => p is HealthPotionMedium) as HealthPotionMedium;
            _healthPotionsBag.Remove(healthPotionMedium);
            return healthPotionMedium != null;
        }
        
        public bool TryGetBigHealthPotion(out HealthPotionBig healthPotionBig)
        {
            healthPotionBig = _healthPotionsBag.FirstOrDefault(p => p is HealthPotionBig) as HealthPotionBig;
            _healthPotionsBag.Remove(healthPotionBig);
            return healthPotionBig != null;
        }
    }
}