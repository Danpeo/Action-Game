using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerLocomotion : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float _movementSpeed = 10.0f;
        [SerializeField] private CharacterController _characterController;

        private Camera _camera;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.MoveAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.MoveAxis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            if (GetCurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                    Warp(savedPosition);
            }
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(GetCurrentLevel(), transform.position.AsVectorData());

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string GetCurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}