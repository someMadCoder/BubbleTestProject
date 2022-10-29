using System.Collections;
using UnityEngine;

namespace Code.Gun
{
    public class LaserPointer:MonoBehaviour
    {
        [SerializeField] private float _lenthgAftherReflection;
        [SerializeField] private LineRenderer _lineRenderer;
        private bool isOn = false;
        public void TurnOn()
        {
            isOn = true;
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
                if (LaserHit().transform.TryGetComponent(out BallGrid ballGrid))
                {
                    ballGrid.ShowEstimatedHitPosition(LaserHit().point);
                }
                else
                {
                    DrawReflection();
                }
                yield return null;
            }
        }

        private void DrawRay()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, LaserHit().point);
        }

        private void DrawReflection() => _lineRenderer.SetPosition(2, ReflectionDirection()*_lenthgAftherReflection);

        private Vector2 ReflectionDirection() => Vector2.Reflect(transform.right, Vector2.up).normalized;

        private RaycastHit2D LaserHit() => Physics2D.Raycast(transform.position, transform.right); // TODO RETURNING BORDER HIT POSITION
    }

    internal class BallGrid
    {
        public void ShowEstimatedHitPosition(Vector2 point)
        {
            throw new System.NotImplementedException();
        }
    }
}