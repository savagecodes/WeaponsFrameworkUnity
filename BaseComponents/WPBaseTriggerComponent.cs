using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPBaseTriggerComponent : WPBaseWeaponComponent
    {
        [Header("Setup")]
        
        [SerializeField]
        private EWPShootType _triggerType;

        [EnumFlag("Desired Trigger State")]
        [SerializeField]
        protected TriggerState _desiredPrimaryTriggerState;

        private int _currentTriggerState = 0;

        private float _currentTimeFromLastShoot;

        private bool _canShoot = true;
        
        #region Getters
        public EWPShootType TriggerType => _triggerType;
        #endregion

        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);

            _baseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT_REQUEST_START,
                x =>
                {
                    _currentTriggerState =
                        Utility.SetBit((int) _currentTriggerState, (int) TriggerState.PRIMARY_PRESSED);
                    _currentTriggerState =
                        Utility.ClearBit((int) _currentTriggerState, (int) TriggerState.PRIMARY_RELEASED);
                });

            _baseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT_REQUEST_STOP,
                x =>
                {
                    _currentTriggerState =
                        Utility.SetBit((int) _currentTriggerState, (int) TriggerState.PRIMARY_RELEASED);
                    _currentTriggerState =
                        Utility.ClearBit((int) _currentTriggerState, (int) TriggerState.PRIMARY_PRESSED);
                    StartCoroutine(ClearTriggerReleasedFlags());
                });
        }

        IEnumerator ClearTriggerReleasedFlags()
        {
            yield return new WaitForEndOfFrame();
            _currentTriggerState = Utility.ClearBit((int) _currentTriggerState, (int) TriggerState.PRIMARY_RELEASED);
            _canShoot = true;
        }

        public override void CustomUpdate(float deltaTime)
        {
            base.CustomUpdate(deltaTime);

            if ((int) _desiredPrimaryTriggerState == _currentTriggerState)
            {
                ProcessFireRequest(deltaTime, _triggerType);
            }
        }

        void ProcessFireRequest(float deltaTime, EWPShootType shootType)
        {
            if (!_canShoot) return;

            if (!_baseWeaponInstance.CanExecuteAction((int) ComponentBlockConditions, true))
                return;

            if (_baseWeaponInstance.WeaponData.FireRate == -1)
            {
                _baseWeaponInstance.EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_SHOOT, shootType);
                _canShoot = false;
                return;
            }

            _currentTimeFromLastShoot += deltaTime;

            if (_currentTimeFromLastShoot > 1 / BaseWeaponInstance.WeaponData.FireRate)
            {
                _baseWeaponInstance.EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_SHOOT, shootType);

                if (_baseWeaponInstance.WeaponData.FireRate == -1)
                {
                    _canShoot = false;
                }

                _currentTimeFromLastShoot = 0;
            }
        }

    }

    public enum EWPShootType
    {
        PRIMARY,
        SECONDARY
    }
}
