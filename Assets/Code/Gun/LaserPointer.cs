using System.Collections;
using Code.BallGridLogic;
using UnityEngine;

namespace Code.Gun
{
    public class LaserPointer:MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        public void DrawRay(Vector3 to)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, to);
            _lineRenderer.SetPosition(2, to);
        }

        public void DrawRay(Vector3 to, Vector3 thenTo)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, to);
            _lineRenderer.SetPosition(2, thenTo);
        }
        
        public void TurnOff()
        {
            _lineRenderer.SetPosition(0,Vector3.zero);
            _lineRenderer.SetPosition(1,Vector3.zero);
            _lineRenderer.SetPosition(2,Vector3.zero);
        }

    }
}