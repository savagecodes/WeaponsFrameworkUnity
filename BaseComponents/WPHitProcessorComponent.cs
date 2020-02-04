using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPHitProcessorComponent : WPBaseWeaponComponent
    {
        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);
            
            BaseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT_HIT,ProcessHit);
        }

        public virtual void ProcessHit(object[] p)
        {
            var hitData = (WeaponHitData) p[0];
            if (hitData.objectHit != null)
            {
                hitData.objectHit.GetComponent<IDamageable>()?.TakeDamage(hitData.damage,BaseWeaponInstance.WeaponCarrier);
            }
            
        }
    }
}
