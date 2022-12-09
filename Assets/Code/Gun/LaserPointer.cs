using System;
using System.Collections;
using System.Collections.Generic;
using Code.BallGridLogic;
using UnityEngine;

namespace Code.Gun
{
    public class LaserPointer:MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        public void TurnOff()
        {
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, transform.position);
        }

        public void DrawLaser(Dictionary<int, Vector3> rayPoints, int maxLaserReflections, float laserLength)
        {
            // _lineRenderer.positionCount = maxLaserReflections;
            // float rayLength = 0;
            // Vector3 previousPoint = transform.position;
            // List<GameObject> gO = new List<GameObject>(rayPoints.Length);
            //
            // foreach (Vector3 point in rayPoints)
            // {
            //     GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //     gO.Add(o);
            //     o.transform.position = point;
            //     Destroy(o,0.1f);
            // }
            //
            //
            // for (int i = 1; i < maxLaserReflections&& i< rayPoints.Length; i++)
            // {
            //     _lineRenderer.SetPosition(i, rayPoints[i]);
            // }
            
            
            // for (var i = 0; i < maxLaserReflections && i< rayPoints.Count; i++)
            // {
            //
            //     rayLength += Vector2.Distance(previousPoint, rayPoints[i]);
            //     
            //     if (rayLength > laserLength)
            //     {
            //         if (i > 0)
            //             previousPoint = rayPoints[i-1];
            //         
            //         DrawRay(rayPoints[i],
            //             (rayPoints[i]-previousPoint).normalized *
            //             (Vector2.Distance(previousPoint, rayPoints[i]) - (rayLength - laserLength)));
            //     }
            //     else
            //     {
            //         DrawRay(rayPoints[i], i+1);
            //     }
            // }
        }

        public void DrawLaser()
        {
            
        }
        private void Start()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.positionCount = 3;
        }

        public void DrawRay(Vector3 position)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, position);
        }

        public void DrawRay(Vector3 position, Vector3 thenTo)
        {
            _lineRenderer.positionCount = 3;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, position);
            _lineRenderer.SetPosition(2, thenTo);
        }

        public void DrawRay(Vector2 direction, float length)
        {
            var position = (Vector2)transform.position + direction * length;
            _lineRenderer.SetPosition(1, position);
        }
    }
}