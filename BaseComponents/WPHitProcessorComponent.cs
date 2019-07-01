using System.Collections;
using System.Collections.Generic;
using SavageCodes.SavageEngine;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPHitProcessorComponent : WPBaseNetworkComponent
    {
        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);
            
            BaseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT_HIT,ProcessHit);
        }

        void ProcessHit(object[] p)
        {
            var hitData = (WeaponHitData) p[0];
            if (hitData.objectHit != null)
            {
                //TODO: Remove Dependency in  Health component of Savage Engine
                
                hitData.objectHit.GetComponent<Health>()?.TakeDamage(0f);
                Debug.Log(hitData.objectHit.name);
            }
            
        }
    }
}
