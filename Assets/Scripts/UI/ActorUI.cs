using Player;
using UnityEngine;

namespace UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        private PlayerHealth _playerHealth;

        private void OnDestroy() => 
            _playerHealth.HealthChanged -= UpdateBar;

        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;

            _playerHealth.HealthChanged += UpdateBar;
        }
        
        private void UpdateBar() => 
            _healthBar.SetValue(_playerHealth.Current, _playerHealth.Max);
    }
}