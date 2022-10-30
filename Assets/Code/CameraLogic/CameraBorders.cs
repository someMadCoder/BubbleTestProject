using System;
using Code.Gun;
using UnityEngine;

namespace Code.CameraLogic
{
    [RequireComponent(typeof(Camera))]
    public class CameraBorders: MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Start()
        {
            float leftBorderX = WorldCameraBorders.Left(_camera);
            float rigthBorderX = WorldCameraBorders.Right(_camera);
            float topBorderY = WorldCameraBorders.Top(_camera);
            float bottomBorderY = WorldCameraBorders.Bottom(_camera);
            var camPosition = transform.position;
            
            Transform topBorder = Instantiate(_collider, transform).transform;
            topBorder.position = new Vector3(camPosition.x, topBorderY + topBorder.localScale.y/2);
            topBorder.localScale = new Vector3((camPosition.x-leftBorderX)*2, 1, 1);
            
            Transform bottomBorder = Instantiate(_collider, transform).transform;
            bottomBorder.position = new Vector3(camPosition.x, bottomBorderY - bottomBorder.localScale.y/2);
            bottomBorder.localScale = topBorder.localScale;
            
            Transform leftBorder = Instantiate(_collider, transform).transform;
            leftBorder.position = new Vector3(leftBorderX- leftBorder.localScale.x/2, camPosition.y );
            leftBorder.localScale = new Vector3(1,(camPosition.y-bottomBorderY)*2,  1);
            
            Transform rightBorder = Instantiate(_collider, transform).transform;
            rightBorder.position = new Vector3(rigthBorderX+ rightBorder.localScale.x/2, camPosition.y );
            rightBorder.localScale = leftBorder.localScale;
        }
    }
}