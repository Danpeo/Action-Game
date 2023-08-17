using Logic;
using UnityEngine;

namespace UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        private IHealth _health;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDestroy() =>
            _health.HealthChanged -= UpdateBar;

        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateBar;
        }

        private void UpdateBar() =>
            _healthBar.SetValue(_health.Current, _health.Max);
    }
}