using System.Linq;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        
        [FormerlySerializedAs("_attackCooldown")] 
        public float AttackCooldown = 1f;
        
        [FormerlySerializedAs("_attackRadius")]
        public float AttackRadius = 0.5f;
        
        [FormerlySerializedAs("_effectiveDistance")] 
        public float EffectiveDistance = 0.5f;
        
        [SerializeField] private float _hitStartPointOffsetY = 0.5f;
        
        [FormerlySerializedAs("_damage")] 
        public float Damage = 10f;

        private Transform _playerTransform;
        private float _currentAttackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        public void Construct(Transform playerTransform) =>
            _playerTransform = playerTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
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
                PhysicsDebug.DrawDebug(GetStartPoint(), AttackRadius, Color.red);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        public void EnableAttack() =>
            _attackIsActive = true;

        public void DisableAttack() =>
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(GetStartPoint(), AttackRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitCount > 0;
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        private Vector3 GetStartPoint() =>
            new Vector3(transform.position.x, transform.position.y + _hitStartPointOffsetY,
                transform.position.z) +
            transform.forward * EffectiveDistance;

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
    }
}