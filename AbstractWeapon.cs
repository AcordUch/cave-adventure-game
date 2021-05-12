using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cave_Adventure
{
    public abstract class AbstractWeapon
    {
        private readonly double _weaponFactor;

        protected AbstractWeapon(double weaponFactor)
        {
            _weaponFactor = weaponFactor;
        }

        public virtual double GetDamage(in Entity entity)
        {
            return entity.Damage * _weaponFactor;
        }
    }
}