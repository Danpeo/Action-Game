using System.Collections;
using System.Linq;
using Infrastructure.Factory;
using Infrastructure.Services;
using Player;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _attackRadius = 0.5f;
        [SerializeField] private float _effectiveDistance = 0.5f;
        [SerializeField] private float _hitStartPointOffsetY = 0.5f;
        [SerializeField] private float _damage = 10f;

        private IGameFactory _gameFactory;
        private Transform _playerTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            _layerMask = 1 << LayerMask.NameToLayer("Player");

            _gameFactory.PlayerCreated += OnPlayerCreated;
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(GetStartPoint(), _attackRadius, Color.red);
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(_damage);
            }
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), _attackRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }

        private Vector3 GetStartPoint() =>
            new Vector3(transform.position.x, transform.position.y + _hitStartPointOffsetY,
                transform.position.z) +
            transform.forward * _effectiveDistance;

        private bool CooldownIsUp() =>
            _currentAttackCooldown <= 0f;

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);
            _animator.PlayAttack_1();

            _isAttacking = true;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private void OnPlayerCreated() =>
            _playerTransform = _gameFactory.PlayerGameObject.transform;
    }
}