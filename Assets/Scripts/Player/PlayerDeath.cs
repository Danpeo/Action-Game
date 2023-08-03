using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerLocomotion))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerLocomotion _locomotion;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private GameObject _deathFx;
        private bool _isDead;

        private void Start() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            
            _locomotion.enabled = false;
            _animator.PlayDeath();

            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}