using System.Collections;
using Infrastructure.Data;
using TMPro;
using UnityEngine;

namespace Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _lootPrefab;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;
        [SerializeField] private float _destroyAfterTime = 1.5f;

        private Loot _loot;
        private WorldData _worldData;
        private bool _picked;

        public void Construct(WorldData worldData) =>
            _worldData = worldData;

        public void Initialize(Loot loot) =>
            _loot = loot;

        private void OnTriggerEnter(Collider other) =>
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;

            UpdateWorldData();
            HideLoot();
            PlayPickupFx();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() =>
            _worldData.LootData.Collect(_loot);

        private void HideLoot() =>
            _lootPrefab.SetActive(true);

        private void PlayPickupFx() =>
            Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity);

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickupPopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(_destroyAfterTime);

            Destroy(gameObject);
        }
    }
}