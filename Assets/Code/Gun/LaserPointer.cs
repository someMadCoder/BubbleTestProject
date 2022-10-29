using System.Collections;
using UnityEngine;

namespace Code.Gun
{
    public class LaserPointer:MonoBehaviour
    {
        [SerializeField] private float _lenthgAftherReflection = 1;
        [SerializeField] private LineRenderer _lineRenderer;
        private bool isOn = false;
        public void TurnOn()
        {
            isOn = true;
            StartCoroutine(Working());
        }
        public void TurnOff()
        {
            isOn = false;
        }

        private IEnumerator Working()
        {
            while (isOn)
            {
                DrawRay();
                // if (LaserHitsBallGrid())
                // {
                //     ballGrid.ShowEstimatedHitPosition(LaserHit().point);
                // }
                // else
                    DrawReflection();
                yield return null;
            }
            _lineRenderer.SetPosition(0,Vector3.zero);
            _lineRenderer.SetPosition(1,Vector3.zero);
            _lineRenderer.SetPosition(2,Vector3.zero);
        }

        private void DrawRay()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, LaserHit().point);
        }

        private void DrawReflection() => _lineRenderer.SetPosition(2, ReflectionDirection());

        private Vector2 ReflectionDirection() => Vector2.Reflect((LaserHit().point - (Vector2)transform.position), LaserHit().normal).normalized*_lenthgAftherReflection + LaserHit().point;
        private Vector2 MaxPosition => Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        private Vector2  MinPosition => Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        private RaycastHit2D LaserHit()
        {
            RaycastHit2D hit;
        
            hit = Physics2D.Raycast(transform.position, transform.right);
            //
            // if (hit.collider == null)
            // {
            //     hit.point = Camera.current.ScreenToWorldPoint(Vector3.Cross(transform.right, MinPosition));
            //     
            // }
            
            return hit;
        
            // TODO RETURNING BORDER HIT POSITION
        }
        // private BallGrid LaserHitsBallGrid()
        // {
        //     RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        //     if (hit.collider&&hit.collider.TryGetComponent(out BallGrid ballGrid))
        //         return ballGrid;
        // }
    }

    internal class BallGrid
    {
        public void ShowEstimatedHitPosition(Vector2 point)
        {
            throw new System.NotImplementedException();
        }
    }
}