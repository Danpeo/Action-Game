using System;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using UnityEngine;

namespace CameraLogic
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private Transform _objectToRotateAround;
        [SerializeField] private float _mouseSensitivityX = 2f;
        [SerializeField] private float _mouseSensitivityY = 2f;
        private float _rotationX;
        private float _rotationY;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void LateUpdate()
        {
            _rotationX = _inputService.LookAxis.x * _mouseSensitivityX;
            _rotationY = _inputService.LookAxis.y * _mouseSensitivityY;
            
            _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

            // Применяем поворот камеры
            Quaternion targetRotation = Quaternion.Euler(_rotationY, _rotationX, 0);
            transform.rotation = targetRotation;
        }

        public void Rotate(GameObject objectToRotateAround)
        {
            _objectToRotateAround = objectToRotateAround.transform;
        }
    }
}