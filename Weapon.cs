using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[RequireComponent(typeof(WPEventsComponent),typeof(WPFireSocketsComponent))]
public class Weapon : MonoBehaviour
{
	[Header("Setup")] 
	[SerializeField] private WeaponData _weaponData;
	private WPBaseWeaponComponent[] _components;
	private WPEventsComponent _eventsComponent;
	private IWeaponCarrier _weaponCarrier;

	public IWeaponCarrier WeaponCarrier => _weaponCarrier;

	public WeaponData WeaponData => _weaponData;
	public WPEventsComponent EventsComponent => _eventsComponent;
	
	[NonSerialized]
	public int CurrentBlockConditions;

	private bool _enabled;

	public bool IsEnabled => _enabled;

	#region Initialization & Setup

	public void InitializeWeapon()
	{
		_eventsComponent = GetComponent<WPEventsComponent>();
		_eventsComponent.Initialize();
		InitializeWeaponComponents();
	//	SavageEngine.instance.UpdateManager.RegisterFixedUpdater(UpdatersID.LEVEL_UPDATER,CustomUpdate);
	}

	void InitializeWeaponComponents()
	{
		_components = GetComponents<WPBaseWeaponComponent>();
		
		for (int i = 0; i < _components.Length; i++)
		{
			_components[i].Initialize(this);
		}
	}

	public void SetEnable(bool state)
	{
		_enabled = state;
		
		EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_WEAPON_STATE_CHANGED,state);
		
	}

	public void SetWeaponCarrier(IWeaponCarrier carrier)
	{
		_weaponCarrier = carrier;
	}
	
	#endregion
	
	void Update()
	{
		
		if(_components == null || _components.Length == 0) return;
		for (int i = 0; i < _components.Length; i++)
		{
			_components[i].CustomUpdate(Time.deltaTime);
		}
	}
	
	#region Handle block Conditions
	bool FlexibleBlockConditionCheck(int preconditions)
	{
		bool InitState = false;

		for (int i = 0; i < (int) ((BlockConditions)CurrentBlockConditions & BlockConditions.NONE) ; i++)
		{
			InitState = InitState || Utility.TestBit((int)CurrentBlockConditions,i) && Utility.TestBit(preconditions, i);
		}

		return !InitState;
	}
	
	bool StrictBlockConditionCheck(int preconditions)
	{
		return CurrentBlockConditions != preconditions;
	}

	bool CheckFlexible(BlockConditions preconditions)
	{
		
		foreach (var item in Enum.GetValues(typeof(BlockConditions)))
		{
			if((int)item == 0) 
				continue;
			
			if (Utility.TestBit(CurrentBlockConditions,(int)item) && Utility.TestBit((int)preconditions,(int)item))
			{
				return false;
			}
		}

		return true;
	}

	public bool CanExecuteAction(int preconditions,bool flexible)
	{
		return !flexible ? StrictBlockConditionCheck(preconditions) : CheckFlexible((BlockConditions) preconditions);
	}
	
	#endregion
	
	#region Handling Inputs

	public void StartShoot()
	{
		EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_SHOOT_REQUEST_START);
	}

	public void StopShoot()
	{
		EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_SHOOT_REQUEST_STOP);
	}

	public void Reload()
	{
		EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_RELOAD_REQUEST);
	}
	
	public void CycleTarget()
	{
		EventsComponent.EventSystem.TriggerEvent(WeaponEventsID.ON_CYCLE_TARGET_REQUEST);
	}
	
	#endregion
	
	void DestroyWeapon()
	{
		for (int i = 0; i < _components.Length; i++)
		{
			_components[i].Destroy();
		}
	}

	private void OnDestroy()
	{
		//SavageEngine.instance.UpdateManager.DeregisterUpdater(UpdatersID.LEVEL_UPDATER,CustomUpdate);
		DestroyWeapon();
	}
}

[System.Flags]
public enum TriggerState
{
	NONE = 0,
	PRIMARY_PRESSED = 1,
	PRIMARY_REALEASED = 2,
	SECONDARY_PRESED = 4,
	SECONDARY_RELEASED = 8
}

[System.Flags]
public enum BlockConditions
{
	NONE = 0,
	IS_RELOADING = 1 << 0,
	IS_OUT_OF_AMMO = 1 << 1,
	IS_OVERHEATED = 1 << 2,
	
}
