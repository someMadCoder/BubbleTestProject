using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Code.BallGridLogic;
using Mono.Cecil;
using UnityEngine;

namespace Code.Gun
{
    [RequireComponent(typeof(LineRenderer))]
    public class GunBehaviour : MonoBehaviour
    {
        [SerializeField] private LaserPointer _laserPointer;
        [SerializeField] private int _laserLength = 15;
        [SerializeField] private int _maxLaserReflections = 3;
        [SerializeField] private Vector3[] _ballWayPoints = new Vector3[10];
        [SerializeField] private int _rotationRestrictionAngle = 70;
        [SerializeField] private Ball _ball;
        private int _iterator;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private float LenghtAfterReflectionFrom(Vector2 firstPoint)
        {
            float length = _laserLength - Vector3.Distance(transform.position, firstPoint);
            return Math.Clamp(length, 0, _laserLength);
        }

        public void AimOn(Vector3 targetPoint)
        {
            transform.rotation = ClampedRotationTo(targetPoint);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _laserLength);
            _laserPointer.DrawRay(hit.point);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Ball ball))
                {
                    if(ball.Grid!=null)
                        ball.Grid.ShowEstimatedHitPosition(hit.point, ball);
                }
                else
                {
                    Vector2 reflectionDirection = Reflection(hit, LenghtAfterReflectionFrom(hit.point));
                    _laserPointer.DrawRay(hit.point, reflectionDirection);
                }
            }
            else
            {
                _laserPointer.DrawRay(transform.up, _laserLength);
            }
        }

        private Quaternion ClampedRotationTo(Vector3 targetPoint)
        {
            float zRotationAngle =
                Vector3.SignedAngle(Vector3.down, (transform.position - targetPoint),
                    Vector3.forward); 
                zRotationAngle = Math.Clamp(zRotationAngle, -_rotationRestrictionAngle, _rotationRestrictionAngle);
            Quaternion rotation = Quaternion.Euler(0, 0, zRotationAngle);
            return rotation;
        }

        public void TryToShoot()
        {
            if (ShotAllowed == false)
                return;
            _previousBallArrived = false;
            _ballWayPoints = CalculatedBallWay(out var hitedGrid);
            Ball ball = Instantiate(_ball, transform.position, transform.rotation);
            ball.MoveAndAttachToGrid(_ballWayPoints, _ballSpeed, hitedGrid, () => _previousBallArrived = true);
        }

        public bool ShotAllowed => _previousBallArrived;

        private Vector3[] CalculatedBallWay(out BallGrid toGrid)
        {
            Trace(out lineRenderer, out toGrid);
            Vector3[] waypoints = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(waypoints);
            return waypoints;
        }

        [SerializeField] private float _maxLength = 1000f ;
        [SerializeField] private int _maxReflections = 100 ;
        [SerializeField] private LineRenderer lineRenderer ;
        private bool _previousBallArrived = true;
        private readonly float _ballSpeed = 5f;


        private void Trace (out LineRenderer line, out BallGrid hitedGrid)
        {
            hitedGrid = null;
            TryGetComponent(out line);
            if (line==null)
            {
                Debug.LogError("Gun does`nt have LineRenderer component!");
            }
            
            Ray ray = new Ray (transform.position, transform.up) ;
            line.positionCount = 1 ;
            line.SetPosition (0, transform.position) ;

            float remainingLength = _maxLength ;
            RaycastHit2D hit ;

            for (int i = 0; i < _maxReflections; i++) {
                hit = Physics2D.Raycast (ray.origin, ray.direction, remainingLength) ;
                line.positionCount += 1 ;
                
                if (hit) 
                {
                    line.SetPosition (line.positionCount - 1, hit.point) ;
                    remainingLength -= Vector2.Distance (ray.origin, hit.point) ;
                    ray = new Ray (hit.point - (Vector2)ray.direction * 0.01f, Vector2.Reflect (ray.direction, hit.normal)) ;
                    if (hit.transform.TryGetComponent(out Ball ball))
                    {
                        line.SetPosition(line.positionCount-1, ball.Grid.GetEstimatedHitPosition(hit.point, ball));
                        hitedGrid = ball.Grid;
                        break;
                    }
                }
                else
                {
                    Debug.LogError("Ray does`t reach any wall or ball grid!");
                }
            }

        }
        private Vector2 Reflection(RaycastHit2D from, float length)
        {
            Vector2 reflection = Vector2.Reflect((from.point - (Vector2)transform.position), -from.normal);
            return reflection.normalized * length + from.point;
        }

        private Vector2 ReflectionDirection(Vector2 direction, RaycastHit2D from)
        {
            Vector2 reflection = Vector2.Reflect(direction, from.normal);
            return reflection.normalized;
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