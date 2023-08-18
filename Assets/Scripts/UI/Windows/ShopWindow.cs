using TMPro;
using UnityEngine;

namespace UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _currentCurrencyText;

        protected override void Initialize()
        {
            UpdateCurrencyText();
        }

        protected override void SubscribeUpdates() => 
            Progress.WorldData.LootData.Changed += UpdateCurrencyText;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= UpdateCurrencyText;
        }

        private void UpdateCurrencyText() => 
            _currentCurrencyText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}