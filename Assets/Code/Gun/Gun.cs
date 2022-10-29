using Code.Infrastructure.Services;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gun
{
    public class Gun: MonoBehaviour
    {
        [SerializeField] private LaserPointer _laserPointer;
        [SerializeField] private UnityEngine.Camera _camera;
        private IInputService _inputService;
        
        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _inputService.OnStartDrag += OnStartDrag;
            _inputService.OnDrag += OnDrag;
            _inputService.OnEndDrag += OnEndDrag;
        }

        private void OnStartDrag()
        {
            
        }
        
        private void OnDrag(PointerEventData eventData)
        {
            Vector3 targetPoint = _camera.ScreenToWorldPoint(eventData.position);
            targetPoint.z = transform.position.z;
            transform.right = -(transform.position - targetPoint).normalized;
        }


        private void OnEndDrag()
        {
            _laserPointer.TurnOff();
        }
        
    }
}