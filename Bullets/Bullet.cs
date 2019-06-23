using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SavageCodes.Frameworks.Weapons
{
    [RequireComponent(typeof(IBulletBehaviour))]
    public class Bullet : MonoBehaviour
    {
        [Header("Bullets Settings")] 
        [SerializeField]
        private float _lifeSpan = 4f;
        private bool _isReady;
        private IBulletComponent[] _components;
        private Weapon _owningWeapon;
        private EventsManager _eventSystem;

        public EventsManager EventSystem => _eventSystem;
        public Weapon OwningWeapon => _owningWeapon;

        void Initialize()
        {
            _eventSystem = new EventsManager();
            _components = GetComponents<IBulletComponent>();

            foreach (var component in _components)
            {
                component.InitializeComponent(this);
            }
            
            EventSystem.TriggerEvent(BulletEventID.ON_BULLET_SPAWN);
        }
        
        public Bullet SetWeapon(Weapon weapon)
        {
            _owningWeapon = weapon;
            return this;
        }

        public Bullet SetDirection(Vector3 dir)
        {
            transform.forward = dir;
            return this;
        }

        public Bullet SetPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }

        public Bullet SetLifeSpan(int lifeSpan)
        {
            _lifeSpan = lifeSpan;
            return this;
        }

        public void Done()
        {
            _isReady = true;
            
            Initialize();
        }

        private void Update()
        {
            if(!_isReady) 
                return;
            
            foreach (var component in _components)
            {
                component.CustomUpdate();
            }
        }

        private void OnDestroy()
        {
            EventSystem.TriggerEvent(BulletEventID.ON_BULLET_DESTROYED);
            
            foreach (var component in _components)
            {
                component.DestroyComponent();
            }
            
            EventSystem.DisponeAllEvents();
        }
    }
}
