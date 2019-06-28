using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SavageCodes.Frameworks.Weapons;
using UnityEngine;
using UnityEngine.Networking;

public class WPNetworkInitializer : WPBaseNetworkComponent
{
    [SyncVar]
    private NetworkInstanceId _weaponCarrierID;

    public override void Initialize(Weapon weapon)
    {
        base.Initialize(weapon);
        
        if (BaseWeaponInstance.WeaponCarrier.IsServer())
        {
            var carrier = (NetworkBehaviour) BaseWeaponInstance.WeaponCarrier;
            
            _weaponCarrierID = carrier.netId;

        }
    }


    void Start()
    {
        if (isServer) 
            return;
        
        if (!ClientScene.objects.ContainsKey(_weaponCarrierID)) 
            
            return;
        
        var weapon = GetComponent<Weapon>();
    
        if(weapon.IsInitialized) 
            return;
        var carrier = ClientScene.objects[_weaponCarrierID];
        
        weapon.SetWeaponCarrier(carrier.GetComponentInChildren<IWeaponCarrier>());
        weapon.InitializeWeapon();
        weapon.SetEnable(true);
        
        //Move to GSNetInitializer
        Solidier solidier = (Solidier)carrier.GetComponentInChildren<IWeaponCarrier>();
        
        solidier.AcquireWeapon(BaseWeaponInstance);

    }
    

    [ClientRpc]
    void RpcInitializeClientWeapon()
    {
        var weapon = GetComponent<Weapon>();
        
        if(weapon.IsInitialized) 
            return;
        
        weapon.SetWeaponCarrier(NetworkServer.FindLocalObject(_weaponCarrierID).GetComponentInChildren<IWeaponCarrier>());
        weapon.InitializeWeapon();
        weapon.SetEnable(true);
    }


}
