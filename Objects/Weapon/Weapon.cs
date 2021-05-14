using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cave_Adventure
{
    public abstract class Weapon
    {
        private readonly double _weaponFactor;

        public int WeaponRadius { get; }

        protected Weapon(double weaponFactor, int weaponRadius)
        {
            _weaponFactor = weaponFactor;
            WeaponRadius = weaponRadius;
        }

        public virtual double GetDamage(in Entity entity)
        {
            return entity.Damage * _weaponFactor;
        }
    }
}