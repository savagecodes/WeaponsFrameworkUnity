using System.Collections;
using System.Collections.Generic;
using SavageCodes.Frameworks.Weapons;
using UnityEngine;
using UnityEngine.Networking;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPBaseNetworkComponent : NetworkBehaviour, IWeaponComponent
    {
        protected Weapon _baseWeaponInstance;

        [SerializeField] [EnumFlag("Block Conditions")]
        protected BlockConditions _blockConditions;

        public BlockConditions ComponentBlockConditions => _blockConditions;

        public Weapon BaseWeaponInstance => _baseWeaponInstance;

        public virtual void Initialize(Weapon weapon)
        {
            _baseWeaponInstance = weapon;
        }

        public virtual void CustomUpdate(float deltaTime)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
