using UnityEngine;

namespace Code.Gun
{
    public class GunBehaviour: MonoBehaviour
    {
        [SerializeField] private LaserPointer _laserPointer;
        [SerializeField] private int _laserLength = 15;
        
        private float LenghtAfterReflectionFrom(Vector2 firstPoint) => _laserLength - Vector3.Distance(transform.position, firstPoint);
        
        public void AimOn(Vector3 targetPoint)
        {
            transform.up = -(transform.position - targetPoint).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _laserLength);
            ShowLaserHint(hit);
        }

        private void ShowLaserHint(RaycastHit2D hit)
        {
            if (hit.collider && hit.transform.TryGetComponent(out Ball ball))
            {
                ball.Grid.ShowEstimatedHitPosition(hit.point, ball);
                
                _laserPointer.DrawRay(hit.point);
            }
            else if (hit.collider != null)
            {
                ShowLaserPointer(hit.point, Reflection(hit, LenghtAfterReflectionFrom(hit.point)));
            }
            else if (hit.collider == null)
            {
                _laserPointer.DrawRay(transform.up * _laserLength + transform.position);
            }
        }

        private Vector2 Reflection(RaycastHit2D from, float length)
        {
            Vector2 reflection = Vector2.Reflect((from.point - (Vector2)transform.position), from.normal);
            return reflection.normalized * length + from.point;
        }

        private void ShowLaserPointer(Vector3 to, Vector3 then)
        {
            _laserPointer.DrawRay(to, then);
        }

        public void TurnOnLaser(Vector3 targetPoint)
        {
            AimOn(targetPoint);
        }
        public void TurnOffLaser()
        {
            _laserPointer.TurnOff();
        }

    }
}