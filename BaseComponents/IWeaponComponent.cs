using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public interface IWeaponComponent
    {
        BlockConditions ComponentBlockConditions { get; }

        Weapon BaseWeaponInstance { get; }

        void Initialize(Weapon weapon);

        void CustomUpdate(float deltaTime);

        void Destroy();

    }
}
