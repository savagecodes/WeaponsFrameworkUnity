using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPRaycasShootComponent : WPBaseShootComponent
    {
        [Header("Setup")] [SerializeField] private float _travelTime;
        [SerializeField] private float _damage;
        [SerializeField] private LayerMask _targetsMask;

        public override void SpawnShoot()
        {
            base.SpawnShoot();

            Transform fireSocket = _fireSocketComponent.GetSocket(0);
            Vector3 target = _aimComponent.GetAimDirection(fireSocket.position);
            SimulateShootWithRaycast(target, fireSocket.position);

        }

        void SimulateShootWithRaycast(Vector3 direction, Vector3 spawnPosition)
        {
            RaycastHit hit;

            if (Physics.Raycast(spawnPosition, direction, out hit, BaseWeaponInstance.WeaponData.Range, _targetsMask))
            {
                SpawnCosmeticBullet((hit.point - spawnPosition).normalized, spawnPosition);
                return;
            }

            SpawnCosmeticBullet(direction, spawnPosition);
        }

        void SpawnCosmeticBullet(Vector3 direction, Vector3 spawnPosition)
        {

            /*Instantiate(_bullet).GetComponent<Bullet>()
                .InitWithotPool()
                .SetParent(SavageEngine.instance.gameManager.enemyesAndBullets.transform)
                .SetRotation(Quaternion.LookRotation(direction))
                .SetAngularVelocity(BaseWeaponInstance.WeaponCarrier.GetRigidBody().angularVelocity)
                .Setvelocity(BaseWeaponInstance.WeaponCarrier.GetRigidBody().velocity)
                .SetPosition(spawnPosition)
                .Done();*/
        }
    }
}
