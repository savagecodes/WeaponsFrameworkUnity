using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPAmmoComponent : WPBaseWeaponComponent
    {

        [Header("Setup")] public int availableAmmo;
        [SerializeField] protected int _maxCapacityPerCharge;
        [SerializeField] protected bool _hasInfiniteAmmo;

        protected int _currentCapacity;

        #region Getters

        public int MaxCapacityPerCharge => _maxCapacityPerCharge;
        public bool HasInfiniteAmmo => _hasInfiniteAmmo;
        public int CurrentCapacity => _currentCapacity;
        public bool IsEmpty => !_hasInfiniteAmmo && _currentCapacity <= 0;

        #endregion

        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);

            _currentCapacity = _maxCapacityPerCharge;
            _baseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_AMMO_CONSUMED,
                x => { ConsumeAmmo((int) x[0]); });
        }

        void ConsumeAmmo(int amount)
        {
            _currentCapacity -= amount > _currentCapacity ? _currentCapacity : amount;
            BaseWeaponInstance.EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_AMMO_UPDATED,
                _currentCapacity);

            if (_currentCapacity == 0)
                Utility.SetBit(_baseWeaponInstance.CurrentBlockConditions, (int) BlockConditions.IS_OUT_OF_AMMO);
        }

    }
}
