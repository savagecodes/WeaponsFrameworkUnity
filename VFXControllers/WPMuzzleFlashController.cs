using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SavageCodes.Frameworks.Weapons
{
    public class WPMuzzleFlashController : MonoBehaviour
    {
        private ParticleSystem[] _muzzleFlashParticles;
        [Header("Settings")]
        [SerializeField]
        private float _maxDuration = 0.2f;

        private Coroutine _forceStopRoutine;

        void Awake()
        {
            _muzzleFlashParticles = GetComponentsInChildren<ParticleSystem>();
        }

        public void Flash()
        {
            foreach (var _muzzle in _muzzleFlashParticles)
            {
                _muzzle.Stop();
                _muzzle.Play();
            }

            if (_forceStopRoutine != null)
            {
                StopCoroutine(_forceStopRoutine);
            }

            _forceStopRoutine = StartCoroutine(ForceMuzzleStop());
        }

        IEnumerator ForceMuzzleStop()
        {
            yield return new WaitForSeconds(_maxDuration);
            foreach (var _muzzle in _muzzleFlashParticles)
            {
                _muzzle.Stop();
            }
        }
    }
}