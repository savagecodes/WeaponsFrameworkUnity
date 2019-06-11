using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
   public class WPReloadComponent : WPAmmoComponent
   {
      [Header("Setup")] [SerializeField] private float _reloadTime;
      [SerializeField] private bool _autoReload;

      private bool _isReloading;

      private Coroutine ReloadRoutine;

      public override void Initialize(Weapon weapon)
      {
         base.Initialize(weapon);
         BaseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_AMMO_UPDATED, x =>
         {
            if (!_isReloading && _autoReload && (int) x[0] == 0)
            {
               Reload();
            }
         });

      }

      void Reload()
      {
         if (!_isReloading)
         {
            ReloadRoutine = StartCoroutine(ProcessReload());
            BaseWeaponInstance.CurrentBlockConditions = Utility.SetBit(BaseWeaponInstance.CurrentBlockConditions,
               (int) BlockConditions.IS_RELOADING);
            _isReloading = true;
         }
      }

      IEnumerator ProcessReload()
      {
         Debug.Log("Reloading");
         yield return new WaitForSeconds(_reloadTime);
         var ammoToLoad = availableAmmo < _maxCapacityPerCharge ? availableAmmo : _maxCapacityPerCharge;
         availableAmmo -= ammoToLoad;
         _currentCapacity += ammoToLoad;
         _isReloading = false;

         BaseWeaponInstance.CurrentBlockConditions = Utility.ClearBit(BaseWeaponInstance.CurrentBlockConditions,
            (int) BlockConditions.IS_RELOADING);
         BaseWeaponInstance.CurrentBlockConditions = Utility.ClearBit(_baseWeaponInstance.CurrentBlockConditions,
            (int) BlockConditions.IS_OUT_OF_AMMO);
      }
   }
}
