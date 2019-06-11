using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RND = UnityEngine.Random;

namespace SavageCodes.Frameworks.Weapons
{
   public class WPAimWithSpreadComponent : WPBaseAimComponent
   {
      [Header("Setup")] [SerializeField] private float _spread;

      public override Vector3 GetAimDirection(Vector3 fromPosition)
      {
         var EndPoint = BaseWeaponInstance.WeaponCarrier.GetPointOfView().Item1 +
                        base.GetAimDirection(fromPosition) * BaseWeaponInstance.WeaponData.Range +
                        RND.insideUnitSphere * _spread;

         return (EndPoint - fromPosition).normalized;
      }
   }
}
