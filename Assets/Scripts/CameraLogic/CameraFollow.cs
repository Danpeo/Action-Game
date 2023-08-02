using Infrastructure.Services;
using Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _targetToFollow;
        [SerializeField] private float _distanceZ = 10f;
        [SerializeField] private float _horizontalOffset;
        [SerializeField] private float _verticalOffset;
        [SerializeField] private float _deadZoneX = 0.1f;
        [SerializeField] private float _deadZoneY = 0.1f;
        [SerializeField] private float _smoothDampTime = 0.1f;
        [SerializeField] private float _mouseSensitivityX = 2f;
        [SerializeField] private float _mouseSensitivityY = 2f;
        [SerializeField] private float _cameraYConstraint = 90f;
        
        private Vector3 _smoothDampVelocity;
        private float _rotationX;
        private float _rotationY;
        private IInputService _inputService;

        private Quaternion _targetRotation;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void LateUpdate()
        {
            if (_targetToFollow == null)
                return;

            UpdateRotation();
            
            Vector3 targetPosition = _targetRotation * new Vector3(0f, 0f, -_distanceZ) + GetFollowPointPosition();

            transform.rotation = _targetRotation;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _smoothDampVelocity, _smoothDampTime);
        }

        private void UpdateRotation()
        {
            _rotationX += _inputService.LookAxis.x * _mouseSensitivityX;
            _rotationY -= _inputService.LookAxis.y * _mouseSensitivityY;

            _rotationY = Mathf.Clamp(_rotationY, -_cameraYConstraint, _cameraYConstraint);

            _targetRotation = Quaternion.Euler(_rotationY, _rotationX, 0); 
        }

        private Vector3 GetFollowPointPosition()
        {
            Vector3 objectToFollowPosition = _targetToFollow.position;
            objectToFollowPosition.x += _horizontalOffset;
            objectToFollowPosition.y += _verticalOffset;

            if (Mathf.Abs(objectToFollowPosition.x) < _deadZoneX)
            {
                objectToFollowPosition.x = 0f;
            }

            if (Mathf.Abs(objectToFollowPosition.y) < _deadZoneY)
            {
                objectToFollowPosition.y = 0f;
            }

            return objectToFollowPosition;
        }

        public void Follow(GameObject objectToFollow)
        {
            _targetToFollow = objectToFollow.transform;
        }
    }
}
