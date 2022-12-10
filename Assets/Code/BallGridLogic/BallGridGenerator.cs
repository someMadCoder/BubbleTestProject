using System.Collections.Generic;
using UnityEngine;

namespace Code.BallGridLogic
{
    /// <summary>
    /// Generator working by wave function collapsed method
    /// </summary>
    public class BallGridWFCGenerator
    {
        [SerializeField] private Ball _ballPrefab;
        private List<Ball> _balls = new List<Ball>(100);
        [field: SerializeField] private IBall _ball;


        public void Generate(int lines, params BallColor[] color)
        {
            for(int x = 0; x<10; x++)
            for (int y = 0; y < lines; y++)
            {
                
            }
        }
         private void Start()
        {
            // Invoke(nameof(DelayConstruct), 1);
            // _ballSize = _ball.GetComponent<SpriteRenderer>().bounds.size;
        }

        private void DelayConstruct()
        {
            // Ball _originOfGrid = Instantiate(_ball, transform);
            // Construct(_originOfGrid);
            LinkSameBalls();
        }

        private void LinkSameBalls()
        {
            foreach (Ball ball in _balls)
            {
                for (int n = 0; n <= 5; n++)
                {
                    // ball.Neighbours[n] = GetBallAt(ball.transform.position + _neighborsOffsets[n]);
                }
            }
        }
        private void Construct(Ball origin)
        {
            _balls.Add(origin);
            // if(_balls.Count>_elementsCount)
                return;
            
            for (int n = 0; n <= 5; n+=Random.Range(1,3))
            {
                // Vector3 newBallPosition = origin.transform.position + _neighborsOffsets[n];
                // if (WidthBallPositionIsCorrect(newBallPosition.x) &&
                //     HeightBallPositionIsCorrect(newBallPosition.y) &&
                //     IsThereIsBallAt(origin.transform.position + _neighborsOffsets[n]) == false)
                // {
                //     var newBall = InstantiateBallNeighbour(origin.transform.position, n);
                //     Construct(newBall);
                // }
            }
        }


        //private Ball InstantiateBallNeighbour(Vector3 originBallPosition, int neighbourToCreateNumber)
        //{
            // Ball newBall = GameObject.Instantiate(_ball, originBallPosition + _neighborsOffsets[neighbourToCreateNumber],
            //     Quaternion.identity);
            // newBall.transform.parent = transform;
            // newBall.Grid = this;
            // int color = Random.Range(0, 2);
            // newBall.Color = (BallColor)color;
            // if ((BallColor)color != BallColor.Yellow)
            // {
            //     newBall.GetComponent<SpriteRenderer>().color = Color.magenta;
            // }
            // return newBall;
        //}
    }
    
}