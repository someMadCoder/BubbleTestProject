using System;
using Code.Infrastructure.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Gun
{
    public class GunController: MonoBehaviour
    {
        [SerializeField] private GunBehaviour _gun;
        private Camera _camera;

        private IInputService _inputService;
        [SerializeField] private LaserPointer _laserPointer;

        public void Init(Camera playerCamera, IInputService inputService)
        {
            _camera = playerCamera;
            _inputService = inputService;
            _laserPointer.Init(_camera);
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
            _gun.TurnOnLaser();
        }

        private void OnDrag(Vector2 pointerPosition)
        {
            RotateTo(pointerPosition);
        }

        private void RotateTo(Vector2 pointerPosition)
        {
            Vector3 targetPoint = _camera.ScreenToWorldPoint(pointerPosition);
            targetPoint.z = transform.position.z;
            _gun.AimOn(targetPoint);
        }


        private void OnPointerUp()
        {
            _gun.TurnOffLaser();
        }
    }
}