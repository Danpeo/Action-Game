using Infrastructure.SaveLoad;
using Infrastructure.Services;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saves");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!_collider)
                return;

            Gizmos.color = new Color32(30, 200, 30, 120);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}