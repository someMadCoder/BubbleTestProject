using UnityEngine;

namespace Code.Gun
{
    public class GunBehaviour: MonoBehaviour
    {
        [SerializeField] private LaserPointer _laserPointer;

        public void AimOn(Vector3 targetPoint)
        {
            transform.right = -(transform.position - targetPoint).normalized;
        }

        public void TurnOnLaser()
        {
            _laserPointer.TurnOn();
        }

        public void TurnOffLaser()
        {
            _laserPointer.TurnOff();
        }
    }
}