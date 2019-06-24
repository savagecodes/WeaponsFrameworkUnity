using System.Collections;
using System.Collections.Generic;
using SavageCodes.Frameworks.Weapons;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public abstract class WPNetworkAttachComponent : WPBaseNetworkComponent
    {
        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);
            AttachToCarrier();
        }

        protected virtual void AttachToCarrier()
        {

        }
    }
}
