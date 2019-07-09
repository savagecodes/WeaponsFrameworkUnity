using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class BulletMovementComponent : BulletBaseComponent
    {
        [Header("Simple Movement Settings")] 
        [SerializeField]
        private float _speed = 5f;
        public override void CustomUpdate()
        {
            base.CustomUpdate();
            transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        }
    }
}
