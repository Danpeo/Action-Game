using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathFx;
        [SerializeField] private float _destroyAfter = 5f;

        public event Action DeathOccured;

        private void Start() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= OnHealthChanged;
            
            _animator.PlayDeath();

            SpawnDeathFx();

            StartCoroutine(DestroyAfter(_destroyAfter));
            
            DeathOccured?.Invoke();
        }

        private void SpawnDeathFx() => 
            Instantiate(_deathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyAfter(float destroyAfter)
        {
            yield return new WaitForSeconds(destroyAfter);
            Destroy(gameObject);
        }
    }
}