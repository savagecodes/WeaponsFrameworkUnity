using System.Collections;
using System.Collections.Generic;
using SavageCodes.Frameworks.Weapons;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class BulletBaseComponent : MonoBehaviour, IBulletComponent
    {
        private Bullet _bullet;

        public Bullet Bullet => _bullet;

        public virtual void InitializeComponent(Bullet bullet)
        {
            _bullet = bullet;
        }

        public virtual void CustomUpdate()
        {

        }

        public virtual void DestroyComponent()
        {

        }
    }
}
