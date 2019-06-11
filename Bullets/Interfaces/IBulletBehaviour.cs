using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public interface IBulletBehaviour
    {

        void EnableBehav();
        void DisableBehav();
        void Explode();
        void Move(int speed, Vector3 dir, Transform target);
    }
}
