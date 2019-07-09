using SavageCodes.SavageEngine;
using UnityEngine;
using UnityEngine.Networking;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPVFXComponent : WPBaseNetworkComponent
    {
        [Header("Required Components")]
        [SerializeField] 
        private Bullet _bulletPrefab;
        [SerializeField]
        private GameObject _metalImpactEffect;
        private WPMuzzleFlashController _muzzleFlashInstance;

        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);

            _baseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_PROJECTILE_SPAWNED,
                SpawnCosmeticBullet);
        }

        void SpawnCosmeticBullet(object[] p)
        {
            //TODO: Handle this properly when predicted weapons are implemented

            var shootResult = (WeaponHitData) p[0];
            
            Vector3 spawnPosition = shootResult.shootPosition;
            Vector3 direction = shootResult.objectHit == null ? shootResult.shootDirection 
                                                              : (shootResult.hitPosition - shootResult.shootPosition).normalized; 

            if (BaseWeaponInstance.WeaponCarrier.IsServer())
            {
                RpcSpawnFX(spawnPosition,direction);
                
                if (shootResult.objectHit != null)
                {
                    RpcSpawnHitFX(shootResult.hitPosition, -direction);
                }
            }
                       
        }
        

        void SpawnHitFX(Vector3 spawnPosition, Vector3 direction)
        {
            Instantiate(_metalImpactEffect,spawnPosition,Quaternion.LookRotation(direction));
        }

        void SpawnFX(Vector3 spawnPosition, Vector3 direction)
        {
            if (_bulletPrefab != null)
            {
                Instantiate(_bulletPrefab).GetComponent<Bullet>()
                    .SetDirection(direction)
                    .SetPosition(spawnPosition)
                    .Done();

            }
             
            if (_muzzleFlashInstance == null)
            {
                _muzzleFlashInstance = GetComponentInChildren<WPMuzzleFlashController>();        
            }
            
            _muzzleFlashInstance.Flash();
                
        }

        [ClientRpc]
        void RpcSpawnHitFX(Vector3 spawnPosition, Vector3 direction)
        {
            SpawnHitFX(spawnPosition,direction);
        }

        [ClientRpc]
        void RpcSpawnFX(Vector3 spawnPosition, Vector3 direction)
        {
            var localSpawnPosition = BaseWeaponInstance.GetComponent<WPFireSocketsComponent>().GetSocket().position;       
            SpawnFX(localSpawnPosition,direction);            
        }
    }
}
