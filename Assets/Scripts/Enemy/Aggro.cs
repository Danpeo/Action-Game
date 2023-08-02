using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follow _follow;
        [SerializeField] private float _cooldown;

        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;

            SwitchFollowOff();
        }

        private void OnTriggerEnter(Collider obj)
        {
            if (_hasAggroTarget)
                return;

            _hasAggroTarget = true;
            
            StopAggroCoroutine();
            
            SwitchFollowOn();
        }

        private void OnTriggerExit(Collider obj)
        {
            if (!_hasAggroTarget)
                return;

            _hasAggroTarget = false;
            
            _aggroCoroutine = StartCoroutine(SwitchFOllowOffAfterCooldown());
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFOllowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
        }

        private void SwitchFollowOff() => 
            _follow.enabled = false;
        
        private void SwitchFollowOn() => 
            _follow.enabled = true;

    }
}