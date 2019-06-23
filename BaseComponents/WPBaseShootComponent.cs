using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPBaseShootComponent : WPBaseWeaponComponent
    {
        [Header("Setup")] 
        [SerializeField] private EWPShootType ShootType;
        [SerializeField] protected Bullet _bullet;
        [SerializeField] protected int _ammoConsumedPerShoot;
        protected WPFireSocketsComponent _fireSocketComponent;
        protected WPBaseAimComponent _aimComponent;



        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);
            _baseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT, ProcessShoot);
            _fireSocketComponent = GetComponent<WPFireSocketsComponent>();
            _aimComponent = GetComponent<WPBaseAimComponent>();
        }

        void ProcessShoot(object[] p)
        {
            //TODO: Add Support for predicted Weapons & FX
            
            if (!BaseWeaponInstance.WeaponCarrier.IsServer())
            {
                return;
            }

            if (ShootType != (EWPShootType) p[0] ||
                !_baseWeaponInstance.CanExecuteAction((int) ComponentBlockConditions, true))
                return;

            _baseWeaponInstance.EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_AMMO_CONSUMED,
                _ammoConsumedPerShoot);
            SpawnShoot();
        }

        protected virtual void SpawnShoot()
        {

        }

    }
}
