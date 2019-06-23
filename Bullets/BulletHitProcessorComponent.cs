using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class BulletHitProcessorComponent : BulletBaseComponent
    {
        private void OnCollisionEnter(Collision other)
        {
            Bullet.EventSystem.TriggerEvent(BulletEventID.ON_BULLET_HIT);
        }
    }
}
