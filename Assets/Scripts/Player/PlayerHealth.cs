using System;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Logic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, IHealth, ISavedProgress
    {
        [SerializeField] private PlayerAnimator _animator;
        private ProgressState _state;

        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHp;
            set
            {
                if (_state.CurrentHp != value)
                {
                    _state.CurrentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            Debug.Log($"TAKE DAMAGE:" + _state.CurrentHp);
            
            _animator.PlayHit();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.PlayerState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHp = Current;
            progress.PlayerState.MaxHp = Max;
        }
    }
}