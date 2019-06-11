using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponCarrier
{
  Rigidbody GetRigidBody();
  Tuple<Vector3,Quaternion> GetPointOfView();
}
