using System;
using Code.Infrastructure.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Gun
{
    public class Gun: MonoBehaviour
    {
        [SerializeField] private LaserPointer _laserPointer;

        [SerializeField] private Camera _camera;

        [SerializeField] private IInputService _inputService;

        public void Init(Camera playerCamera, IInputService inputService)
        {
            _camera = playerCamera;
            _inputService = inputService;
            SubscribeInputEvents();
        }

        private void SubscribeInputEvents()
        {
            _inputService.OnPointerDown += OnPointerDown;
            _inputService.OnDrag += OnDrag;
            _inputService.OnPointerUp += OnPointerUp;
        }

        private void OnPointerDown()
        {
            _laserPointer.TurnOn();
        }

        private void OnDrag(Vector2 pointerPosition)
        {
            RotateTo(pointerPosition);
        }

        private void RotateTo(Vector2 pointerPosition)
        {
            Vector3 targetPoint = _camera.ScreenToWorldPoint(pointerPosition);
            targetPoint.z = transform.position.z;
            transform.right = -(transform.position - targetPoint).normalized;
        }


        private void OnPointerUp()
        {
            _laserPointer.TurnOff();
        }
    }
}