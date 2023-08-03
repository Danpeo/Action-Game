using System;
using Logic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        private static readonly int IsMoving = Animator.StringToHash("IsWalking");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _walkingStateHash = Animator.StringToHash("MoveTree");
        private readonly int _deathStateHash = Animator.StringToHash("Sword1h_Death_2");


        private void Update()
        {
            //_animator.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
            Move();
        }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        public void PlayHit() =>
            _animator.SetTrigger(Hit);

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        private void Move()
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
        
        public void StopMoving() => _animator.SetBool(IsMoving, false);
        
        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }
    
        public void ExitedState(int stateHash) => 
            StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
      
            return state;
        }
    }
}