using System.Collections;
using System.Collections.Generic;
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
            Debug.Log(((RaycastHit)p[0]).collider.gameObject.name);
        }
    }
}
