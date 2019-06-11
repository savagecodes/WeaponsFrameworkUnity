using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPBaseWeaponComponent : MonoBehaviour
{
    protected Weapon _baseWeaponInstance;

    [SerializeField] 
    [EnumFlag("Block Conditions")] 
    protected BlockConditions ComponentBlockConditions;

    public Weapon BaseWeaponInstance => _baseWeaponInstance;

    public virtual void Initialize(Weapon weapon)
    {
        _baseWeaponInstance = weapon;
    }

    public virtual void CustomUpdate(float deltaTime)
    {
        
    }

    public virtual void Destroy()
    {
        
    }
}
