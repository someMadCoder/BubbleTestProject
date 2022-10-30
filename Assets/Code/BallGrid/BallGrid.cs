using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.BallGridLogic
{
    public class BallGrid: MonoBehaviour
    {
        [SerializeField]private Ball _ball;
        [SerializeField]private GameObject _estimatedHitMarker;
        public bool construct;
        private List<Ball> _balls= new List<Ball>(100);
        private Vector3[] neighborsOffsets =
        {
            new Vector2(0.75f, 1.25f),
            new Vector2(1.5f, 0f),
            new Vector2(0.75f, -1.25f),
            new Vector2(-0.75f, -1.25f),
            new Vector2(-1.5f, 0f),
            new Vector2(-0.75f, 1.25f),
        };

        private void Start()
        {
            Invoke(nameof(DelayConstruct), 1);
        }

        private void DelayConstruct()
        {
            Ball ball = Instantiate(_ball, transform);
            Construct(ball);
            LinkSameBalls();
            
        }

        private void LinkSameBalls()
        {
            foreach (Ball ball in _balls)
            {
                for (int n = 0; n <= 5; n++)
                {
                    ball.neighbors[n] = GetBallAt(ball.transform.position + neighborsOffsets[n]);
                }
            }
        }

        private const float TOLERANCE = 0.1f;

        private void Construct(Ball origin)
        {
            _balls.Add(origin);
            if(_balls.Count>50)
                return;
            
            for (int n = 0; n <= 5; n++)
            {
                if (origin.neighbors[n] == null &&
                    (origin.transform.position + neighborsOffsets[n]).x < WorldCameraBorders.Right(Camera.main) &&
                    (origin.transform.position + neighborsOffsets[n]).x > WorldCameraBorders.Left(Camera.main) &&
                    (origin.transform.position + neighborsOffsets[n]).y < WorldCameraBorders.Top(Camera.main) &&
                    (origin.transform.position + neighborsOffsets[n]).y > WorldCameraBorders.Bottom(Camera.main) &&
                    IsThereIsBallIn(origin.transform.position + neighborsOffsets[n]) == false &&
                    Random.value > 0.5f)
                {
                    Ball newBall = Instantiate(_ball, origin.transform.position + neighborsOffsets[n], Quaternion.identity);
                    newBall.transform.parent = transform;
                    newBall.Grid = this;
                    Construct(newBall);
                }
            }
        }

        private bool IsThereIsBallIn(Vector2 position)
        {
            return _balls.Any(b => Math.Abs(b.transform.position.x - position.x) < TOLERANCE &&
                                   Math.Abs(b.transform.position.y - position.y) < TOLERANCE);
        }
        private Ball GetBallAt(Vector2 position)
        {
            return _balls.FirstOrDefault(b => Math.Abs(b.transform.position.x - position.x) < TOLERANCE &&
                                   Math.Abs(b.transform.position.y - position.y) < TOLERANCE);
        }

        private int Loop(int value, int max)
        {
            if (value <= max)
                return value;
            return Math.Abs(max - value);

        }


        public void ShowEstimatedHitPosition(Vector2 point, Ball ball)
        {
            
            Vector3[] possiblePositions = new Vector3[6];
            for (int n = 0; n <= 5; n++)
            {
                Vector3 neigbhorPosition = ball.transform.position + neighborsOffsets[n];
                if (IsThereIsBallIn(neigbhorPosition)==false)
                {
                    possiblePositions[n] = neigbhorPosition;
                }
            }

            Vector3 estimatedHitPosition = possiblePositions.OrderBy(p => Vector3.Distance(p, point)).First();
            _estimatedHitMarker.SetActive(true);
            _estimatedHitMarker.transform.position = estimatedHitPosition;
        }
    }
}