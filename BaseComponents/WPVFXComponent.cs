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

        public override void Initialize(Weapon weapon)
        {
            base.Initialize(weapon);

            _baseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_PROJECTILE_SPAWNED,
                SpawnCosmeticBullet);
        }

        void SpawnCosmeticBullet(object[] p)
        {
            var spawnPosition = ((Tuple<Vector3, Vector3>) p[0]).Item1;
            var direction = ((Tuple<Vector3, Vector3>) p[0]).Item2;     
            RpcSpawnBullet(spawnPosition,direction);
            
        }

        [ClientRpc]
        void RpcSpawnBullet(Vector3 spawnPosition, Vector3 direction)
        {
            Instantiate(_bulletPrefab).GetComponent<Bullet>()
                .SetDirection(direction)
                .SetPosition(spawnPosition)
                .Done();
        }
    }
}
