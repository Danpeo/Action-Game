using System;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        private ProgressState _state;

        public Action HealthChanged;

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
            
            //TODO: Play Animation
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