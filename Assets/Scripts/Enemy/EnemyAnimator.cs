using System;
using Logic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private static readonly int Attack_1 = Animator.StringToHash("Attack_1");
        private static readonly int Attack_2 = Animator.StringToHash("Attack_2");
        private static readonly int Attack_3 = Animator.StringToHash("Attack_3");
        private static readonly int Attack_4 = Animator.StringToHash("Attack_4");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attack_1StateHash = Animator.StringToHash("Attack 1");
        private readonly int _attack_2StateHash = Animator.StringToHash("Attack 2");
        private readonly int _attack_3StateHash = Animator.StringToHash("Attack 3");
        private readonly int _attack_4StateHash = Animator.StringToHash("Attack 4");
        private readonly int _walkingStateHash = Animator.StringToHash("Walk");
        private readonly int _deathStateHash = Animator.StringToHash("Death");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public AnimatorState State { get; private set; }

        public void PlayHit() =>
            _animator.SetTrigger(Hit);

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void StartMoving() => 
            _animator.SetBool(IsMoving, true);
        
        public void StopMoving() => 
            _animator.SetBool(IsMoving, false);
        
        public void PlayAttack_1() => 
            _animator.SetTrigger(Attack_1);

        public void PlayAttack_2() => 
            _animator.SetTrigger(Attack_2);

        public void PlayAttack_3() => 
            _animator.SetTrigger(Attack_3);

        public void PlayAttack_4() => 
            _animator.SetTrigger(Attack_4);

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
            else if (stateHash == _attack_1StateHash)
                state = AnimatorState.Attack_1;
            else if (stateHash == _attack_2StateHash)
                state = AnimatorState.Attack_2;
            else if (stateHash == _attack_3StateHash)
                state = AnimatorState.Attack_3;
            else if (stateHash == _attack_4StateHash)
                state = AnimatorState.Attack_4;
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