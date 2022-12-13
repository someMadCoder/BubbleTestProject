using System;
using System.Collections.Generic;
using System.Linq;
using Code.CameraLogic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.BallGridLogic
{
    /// <summary>
    /// Generator working by wave function collapsed method
    /// </summary>
    public class BallGridGenerator
    {
        private readonly IBall[] _balls;
        private readonly Grid<IBall> _grid;
        private readonly int _maxElementsCount = 100;
        private readonly BallGrid _ballGrid;
        private readonly int _ballsNumberInWidth;
        private readonly float _startLowestElementPosition;

        public BallGridGenerator(IBall[] balls, BallGrid ballGrid, float startLowestElementPosition, int lines, int ballsNumberInWidth = 10)
        {
            _ballsNumberInWidth = ballsNumberInWidth;
            _ballGrid = ballGrid;
            _startLowestElementPosition = startLowestElementPosition;
            _balls = balls;
            _maxElementsCount = lines * ballsNumberInWidth;
            //float height = ;
            _grid = new Grid<IBall>(_maxElementsCount, _balls[0].Size,
                _startLowestElementPosition, 0, lines * 1.25f);
            _ballGrid._grid = _grid;
        }

        public void Generate(float worldSizeScreenWidth, params BallColor[] color)
        {
            var startPosition = StartGenerationElementPosition(worldSizeScreenWidth);

            IBall ball = GameObject.Instantiate(_balls[0].gameObject, startPosition, Quaternion.identity)
                .GetComponent<IBall>();
            ball.Grid = _ballGrid;
            //Construct(ball);
            InstantiateSuitableNeighbours(ball);
        }

        private Vector3 StartGenerationElementPosition(float worldSizeScreenWidth)
        {
            float x = worldSizeScreenWidth - (((_ballsNumberInWidth) * _grid.XOffset));
            Vector3 startPosition = new Vector3(WorldCameraBorders.Left(Camera.main) + x + _balls[0].Size.x / 2,
                _startLowestElementPosition, 0);
            return startPosition;
        }

        private void InstantiateSuitableNeighbours(IBall ball)
        {
            _grid.AttachElement(ball);
            if (_grid.ElementsCount > _maxElementsCount)
                return;

            for (var n = 0; n < 6; n++)
            {
                Vector3 newBallPosition = _grid.GetNeighbourPosition(ball.Position, n);
                IBall newBall = null;
                if (_grid.IsPositionFreeAndCorrect(newBallPosition))
                    if (ball.Neighbours.Count(n => n != null && n.Color == ball.Color) < 3)
                    {
                        newBall = ball.Neighbours[n] = InstantiateNeighbourFor
                            (ball, newBallPosition);
                        //InstantiateSuitableNeighbours(ball.Neighbours[n]);
                    }
                    else
                    {
                        newBall = InstantiateNeighbourFor(_balls[Random.Range(0, _balls.Length)], newBallPosition);
                    }

                if (newBall != null)
                {
                    newBall.Grid = _ballGrid;
                    InstantiateSuitableNeighbours(newBall);
                }
            }

            // foreach (var neighbour in ball.Neighbours)
            // {
            //     if (neighbour != null)
            //     {
            //           
            //         InstantiateSuitableNeighbours(neighbour);
            //     }
            // }
        }

        private IBall InstantiateNeighbourFor(IBall ball, Vector3 neighbourPosition)
        {
            return GameObject.Instantiate(ball.gameObject, neighbourPosition, Quaternion.identity)
                .GetComponent<IBall>();
        }

        private void Construct(IBall origin)
        {
            _grid.AttachElement(origin);
            if (_grid.ElementsCount > _maxElementsCount)
                return;

            for (int n = 0; n < 6; n += Random.Range(1, 3))
            {
                Vector3 newBallPosition = _grid.GetNeighbourPosition(origin.Position, n);
                if (_grid.IsPositionFreeAndCorrect(newBallPosition))
                {
                    var newBall =
                        InstantiateBallNeighbour(_balls[Random.Range(0, _balls.Length)], origin.Position,
                            n); //InstantiateNeighbourFor(origin, newBallPosition);
                    Construct(newBall);
                }
            }
        }


        private IBall InstantiateBallNeighbour(IBall ball, Vector3 originBallPosition, int neighbourToCreateNumber)
        {
            IBall newBall = GameObject.Instantiate(ball.gameObject,
                    _grid.GetNeighbourPosition(originBallPosition, neighbourToCreateNumber), Quaternion.identity)
                .GetComponent<IBall>();
            newBall.gameObject.transform.SetParent(_ballGrid.transform);
            return newBall;
        }
    }
}