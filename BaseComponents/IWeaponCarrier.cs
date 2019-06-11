using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
  public interface IWeaponCarrier
  {
    Rigidbody GetRigidBody();
    Tuple<Vector3, Quaternion> GetPointOfView();
  }
}
