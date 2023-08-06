using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Logic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerAnimator))]
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private CharacterController _characterController;
        private IInputService _inputService;

        private static int _layerMask;
        private Collider[] _hits = new Collider[10];
        private PlayerStats _stats;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && !_playerAnimator.IsAttacking)
                _playerAnimator.PlayAttack_1();
        }

        private void OnAttack()
        {
            PhysicsDebug.DrawDebug(GetStartPoint() + transform.forward, _stats.AttackRadius, Color.red);
            for (int i = 0; i < GetHits(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
            }
        }

        public void LoadProgress(PlayerProgress progress) => 
            _stats = progress.PlayerStats;

        private int GetHits() => 
            Physics.OverlapSphereNonAlloc(GetStartPoint() + transform.forward, _stats.AttackRadius, _hits, _layerMask);

        private Vector3 GetStartPoint() => 
            new(transform.position.x, _characterController.center.y / 2, transform.position.z);
    }
}