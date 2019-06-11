using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    [RequireComponent(typeof(IBulletBehaviour))]
    public class Bullet : MonoBehaviour/*, IManejable*/
    {
        [Header("Bullets Settings")] 
        public string id;
        public int speed;
        public int lifeSpan;

        protected Transform _targetData;
        IBulletBehaviour behav;
        //SimpleTimmer timmer;
        private Rigidbody _rb;
        bool _isReady;
        private Vector3 _dir;

        public string ID
        {
            get { return id; }
        }

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            behav = GetComponent<IBulletBehaviour>();
        }


        public void Destroy()
        {

            Destroy(this.gameObject);
        }

        /*public IManejable Clone()
        {

            return Instantiate(this.gameObject).GetComponent<Bullet>();
        }*/

        public Bullet SetFoward(Vector3 forward)
        {
            transform.forward = forward;
            _rb.rotation = Quaternion.LookRotation(forward);
            return this;
        }

        public Bullet SetDirection(Vector3 dir)
        {
            transform.forward = dir;
            _rb.rotation = Quaternion.LookRotation(dir);
            _dir = dir;
            return this;
        }

        public Bullet Setvelocity(Vector3 vel)
        {
            _rb.velocity = vel;
            return this;
        }

        public Bullet SetPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }

        public Bullet SetRotation(Quaternion r)
        {
            // Debug.Log(r);
            transform.rotation = r;
            return this;
        }

        public Bullet SetAngularVelocity(Vector3 v)
        {
            _rb.angularVelocity = v;
            return this;
        }

        public Bullet SetTarget(Transform target)
        {
            _targetData = target;
            return this;
        }


        public Bullet SetLifeSpan(int l)
        {
            lifeSpan = l;
            return this;
        }

        public Bullet SetSpeed(int speed)
        {
            this.speed = speed;
            return this;
        }

        public void Done()
        {
            _isReady = true;
        }

        void CustomFixedUpdate()
        {
            if (!_isReady) return;

            if (behav != null) behav.Move(speed, _dir, _targetData);
        }

       /* void CustomUpdate()
        {
            if (timmer != null) timmer.Update();

        }*/

        public void Init()
        {
            if (_rb == null)
                _rb = gameObject.AddComponent<Rigidbody>();
            behav = GetComponent<IBulletBehaviour>();
            //timmer = new SimpleTimmer();
            //timmer.Create(lifeSpan, behav.Explode);
            behav.EnableBehav();
            //SavageEngine.Instance.UpdateManager.RegisterUpdater(UpdatersID.BULLETS_UPDATER, CustomUpdate);
            //SavageEngine.Instance.UpdateManager.RegisterFixedUpdater(UpdatersID.BULLETS_UPDATER, CustomFixedUpdate);
            gameObject.SetActive(true);
        }

        public Bullet InitWithotPool()
        {
            Init();
            return this;
        }

        public void Dispose()
        {
            _isReady = false;
            _rb.angularVelocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
            behav.DisableBehav();
            /*if (timmer != null) timmer.Destroy();
            SavageEngine.Instance.UpdateManager.DeRegisterUpdater(UpdatersID.BULLETS_UPDATER, CustomUpdate);
            SavageEngine.Instance.UpdateManager.DeregisterFixedUpdater(UpdatersID.BULLETS_UPDATER, CustomFixedUpdate);*/
            gameObject.SetActive(false);

        }

      /*  private void OnDestroy()
        {
           SavageEngine.Instance.UpdateManager.DeRegisterUpdater(UpdatersID.BULLETS_UPDATER, CustomUpdate);
            SavageEngine.Instance.UpdateManager.DeregisterFixedUpdater(UpdatersID.BULLETS_UPDATER, CustomFixedUpdate);
        }*/
    }
}
