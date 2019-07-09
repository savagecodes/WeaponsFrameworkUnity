using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPRaycasShootComponent : WPBaseShootComponent
    {
        [Header("Setup")] 
        [SerializeField] private float _travelTime;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _targetsMask;

        protected override void SpawnShoot()
        {
            base.SpawnShoot();

            Transform fireSocket = _fireSocketComponent.GetSocket(0);
            Vector3 target = _aimComponent.GetAimDirection(fireSocket.position);

            var rayShootResult = SimulateShootWithRaycast(target, fireSocket.position);
            
            _baseWeaponInstance.EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_PROJECTILE_SPAWNED,rayShootResult);

        }

        WeaponHitData SimulateShootWithRaycast(Vector3 direction, Vector3 spawnPosition)
        {
            RaycastHit hit;
            WeaponHitData hitData = new WeaponHitData();
            
            hitData.shootPosition = spawnPosition;
            hitData.shootDirection = direction;

            if (Physics.Raycast(spawnPosition, direction, out hit, BaseWeaponInstance.WeaponData.Range, _targetsMask))
            {
                hitData.hitPosition = hit.point;
                hitData.objectHit = hit.collider.gameObject;         
                BaseWeaponInstance.EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_SHOOT_HIT,hitData);
            }

            return hitData;
        }

   
    }
}
