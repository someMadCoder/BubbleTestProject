using UnityEngine;

namespace Code.Camera
{
    /// <summary>
    /// Keeps constant camera width instead of height, works for both Orthographic & Perspective cameras
    /// </summary>
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Vector2 _defaultResolution = new Vector2(720, 1280);
        [Range(0f, 1f)] public float WidthOrHeight = 0;
    
        private float _initialSize;
        private float _targetAspect;
        private void Awake()
        {
            _initialSize = _camera.orthographicSize;
            _targetAspect = _defaultResolution.x / _defaultResolution.y;
        }
        
        private void Update()
        {
            AdjustWidth();
        }

        private void AdjustWidth()
        {
            float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
            _camera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, WidthOrHeight);
        }

    }
}