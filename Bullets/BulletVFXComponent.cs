using System.Collections;
using System.Collections.Generic;
using SavageCodes.Frameworks.Weapons;
using UnityEngine;

public class BulletVFXComponent : BulletBaseComponent
{
    [Header("Prefab Effects")] 
    [SerializeField]
    private GameObject _bulletTrail;
    [SerializeField]
    private GameObject _metalImpactEffect;
    
    private void OnCollisionEnter(Collision other)
    {
        Instantiate(_metalImpactEffect,other.GetContact(0).point,Quaternion.LookRotation(-transform.forward));
        Destroy(this.gameObject);
    }
}
