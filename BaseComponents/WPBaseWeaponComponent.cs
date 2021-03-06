﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPBaseWeaponComponent : MonoBehaviour,IWeaponComponent
    {
        protected Weapon _baseWeaponInstance;
        [SerializeField]
        [EnumFlag("Block Conditions")]
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
