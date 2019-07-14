using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
   public struct WeaponHitData
   {
      public Vector3 shootPosition;
      public Vector3 shootDirection;
      public Vector3 hitPosition;
      public float hitTimeInSeconds;
      public GameObject objectHit;
      public float damage;
   }
}
