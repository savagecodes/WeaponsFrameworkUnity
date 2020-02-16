using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
   public class WPCoolDownComponent : WPBaseWeaponComponent
   {

      [Header("Setup")] 
      [SerializeField] private float _maxHeatAllowed;
      [SerializeField] private float _heatGeneratedPerShoot;
      [SerializeField] private float _timeToCoolDown;
      [SerializeField] private float _timeBeforeStartCoollingDown;

      private float _currentHeat;
      private float _timeSinceLastShoot;
      private float _coolDownRate;
      private bool _isCollingDown;
      
      public float MaxHeatAllowed => _maxHeatAllowed;
      public float CurrentHeat => _currentHeat;
      public bool IsCollingDown => _isCollingDown;

      public override void Initialize(Weapon weapon)
      {
         base.Initialize(weapon);
         BaseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT, x => AddHeat());

      }

      void AddHeat()
      {
         _currentHeat += _heatGeneratedPerShoot;
         _timeSinceLastShoot = 0;

         if (_currentHeat >= _maxHeatAllowed)
         {
            BaseWeaponInstance.CurrentBlockConditions = Utility.SetBit(BaseWeaponInstance.CurrentBlockConditions,
               (int) BlockConditions.IS_OVERHEATED);
         }
      }

      void CoolDown()
      {
         if (_currentHeat <= 0)
         {
            BaseWeaponInstance.CurrentBlockConditions = Utility.ClearBit(BaseWeaponInstance.CurrentBlockConditions,
               (int) BlockConditions.IS_OVERHEATED);
            _currentHeat = 0;
            return;
         }

         _coolDownRate = _maxHeatAllowed / _timeToCoolDown * Time.deltaTime;
         _currentHeat -= _coolDownRate;
      }

      public override void CustomUpdate(float deltaTime)
      {
         base.CustomUpdate(deltaTime);

         _timeSinceLastShoot += deltaTime;

         if (_timeSinceLastShoot > _timeBeforeStartCoollingDown)
         {
            CoolDown();
         }

      }
   }
}
