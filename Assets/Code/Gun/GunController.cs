using System;
using Code.Infrastructure.Services;
using Code.Services.Input;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Gun
{
    public class GunController: MonoBehaviour
    {
        [SerializeField] private GunBehaviour _gun;
        private Camera _camera;
        private IInputService _inputService;
        
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

        private void OnPointerDown(Vector2 pointerPosition)
        {
            _gun.TurnOnLaser(pointerPosition);
        }

        private void OnPointerUp(Vector2 pointerPosition)
        {
            _gun.TurnOffLaser();
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


    }
}