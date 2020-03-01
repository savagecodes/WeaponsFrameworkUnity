using System.Collections;
using System.Collections.Generic;
using SavageCodes.Frameworks.Weapons;
using UnityEngine;

public class WPSFXComponent : WPBaseWeaponComponent
{
    [SerializeField]
    private AudioSource _shootSoundSource;
    
    public override void Initialize(Weapon weapon)
    {
        base.Initialize(weapon);
        BaseWeaponInstance.EventsComponent.EventSystem.SubscribeToEvent(WeaponEventsID.ON_SHOOT,OnShoot);
    }

    protected virtual void OnShoot(object[] p)
    {
        if (_shootSoundSource)
        {
            _shootSoundSource.Play();
        }
    }
    
}
