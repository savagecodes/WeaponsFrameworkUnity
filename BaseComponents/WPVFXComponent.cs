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
        private GameObject _muzzleFlash;

        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);

            _baseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_PROJECTILE_SPAWNED,
                SpawnCosmeticBullet);
        }

        void SpawnCosmeticBullet(object[] p)
        {
            //TODO: Handle this properly when predicted weapons are implemented
            
            if(isLocalPlayer) return;
            
           /* var spawnPosition = ((Tuple<Vector3, Vector3>) p[0]).Item1;
            var direction = ((Tuple<Vector3, Vector3>) p[0]).Item2;     
            RpcSpawnBullet(spawnPosition,direction);*/
            
        }

        [ClientRpc]
        void RpcSpawnBullet(Vector3 spawnPosition, Vector3 direction)
        {
            if (_bulletPrefab == null || _muzzleFlash == null)
                return;
            
            Instantiate(_bulletPrefab).GetComponent<Bullet>()
                .SetDirection(direction)
                .SetPosition(spawnPosition)
                .Done();

            Instantiate(_muzzleFlash, spawnPosition, Quaternion.LookRotation(direction));
        }
    }
}
