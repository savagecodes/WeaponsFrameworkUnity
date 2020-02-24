using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
   public class WPBaseAimComponent : WPBaseWeaponComponent
   {
      [SerializeField] 
      protected bool isMultiTargetWeapon;
      public bool IsMultiTargetWeapon => isMultiTargetWeapon;
      
      protected Transform _fakeTarget;

      public virtual Transform GetAimTarget(Vector3 fromPosition)
      {
         return SetUpFakeTarget(fromPosition);
      }
      
      public virtual List<Transform> GetAimTargets(Vector3 fromPosition)
      {
         var targets = new List<Transform>();
         targets.Add(SetUpFakeTarget(fromPosition));
         return targets;
      }

      public virtual Vector3 GetAimDirection(Vector3 fromPosition)
      {
         return (_baseWeaponInstance.WeaponCarrier.GetPointOfView().Item2 * Vector3.forward);
      }

      Transform SetUpFakeTarget(Vector3 fromPosition)
      {
         if (_fakeTarget == null)
         {
            _fakeTarget = new GameObject().transform;
         }

         _fakeTarget.position = fromPosition +
                                (_baseWeaponInstance.WeaponCarrier.GetPointOfView().Item2 * Vector3.forward) *
                                _baseWeaponInstance.WeaponData.Range;
         return _fakeTarget;
      }
   }
}
