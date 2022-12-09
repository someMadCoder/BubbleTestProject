using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.CameraLogic;
using Code.Gun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.BallGridLogic
{
    public class BallGrid: MonoBehaviour
    {
        private const float TOLERANCE = 0.1f;
        [SerializeField] private Ball _ball;
        [SerializeField] private GameObject _estimatedHitMarker;
        [SerializeField] private int _ballCount = 100;
        private List<Ball> _balls= new List<Ball>(100);
        private Ball _originOfGrid;

        private Vector3[] _neighborsOffsets =
        {
            new Vector2(0.75f, 1.25f),
            new Vector2(1.5f, 0f),
            new Vector2(0.75f, -1.25f),
            new Vector2(-0.75f, -1.25f),
            new Vector2(-1.5f, 0f),
            new Vector2(-0.75f, 1.25f),
        };

        private Vector3 _ballSize;


        private void Start()
        {
            Invoke(nameof(DelayConstruct), 1);
            _ballSize = _ball.GetComponent<SpriteRenderer>().bounds.size;
        }

        private void DelayConstruct()
        {
            _originOfGrid = Instantiate(_ball, transform);
            Construct(_originOfGrid);
            LinkSameBalls();
        }

        private void LinkSameBalls()
        {
            foreach (Ball ball in _balls)
            {
                for (int n = 0; n <= 5; n++)
                {
                    ball.Neighbours[n] = GetBallAt(ball.transform.position + _neighborsOffsets[n]);
                }
            }
        }

        private Ball InstantiateBallNeighbour(Vector3 originBallPosition, int neighbourToCreateNumber)
        {
            Ball newBall = Instantiate(_ball, originBallPosition + _neighborsOffsets[neighbourToCreateNumber],
                Quaternion.identity);
            newBall.transform.parent = transform;
            newBall.Grid = this;
            int color = Random.Range(0, 2);
            newBall.Color = (BallColor)color;
            if ((BallColor)color != BallColor.Yellow)
            {
                newBall.GetComponent<SpriteRenderer>().color = Color.magenta;
            }
            return newBall;
        }

        private void Construct(Ball origin)
        {
            _balls.Add(origin);
            if(_balls.Count>_ballCount)
                return;
            
            for (int n = 0; n <= 5; n+=Random.Range(1,3))
            {
                Vector3 newBallPosition = origin.transform.position + _neighborsOffsets[n];
                if (WidthBallPositionIsCorrect(newBallPosition.x) &&
                    HeightBallPositionIsCorrect(newBallPosition.y) &&
                    IsThereIsBallAt(origin.transform.position + _neighborsOffsets[n]) == false)
                {
                    var newBall = InstantiateBallNeighbour(origin.transform.position, n);
                    Construct(newBall);
                }
            }
        }

        private bool HeightBallPositionIsCorrect(float yPosition)
        {
            return yPosition > Camera.main.transform.position.y &&
                   (transform.position.y + TOLERANCE - yPosition) >= 0;
        }

        private bool WidthBallPositionIsCorrect(float xPosition)
        {
            return xPosition + _ballSize.x/2 < WorldCameraBorders.Right(Camera.main) &&
                   xPosition - _ballSize.x/2 > WorldCameraBorders.Left(Camera.main);
        }

        private bool IsThereIsBallAt(Vector2 position)
        {
            return _balls.Any(b => Math.Abs(b.transform.position.x - position.x) < TOLERANCE &&
                                   Math.Abs(b.transform.position.y - position.y) < TOLERANCE);
        }
        private Ball GetBallAt(Vector2 position)
        {
            return _balls.FirstOrDefault(b => Math.Abs(b.transform.position.x - position.x) < TOLERANCE &&
                                   Math.Abs(b.transform.position.y - position.y) < TOLERANCE);
        }

        public void ShowEstimatedHitPosition(Vector2 point, Ball hitedBall)
        {
            _estimatedHitMarker.SetActive(true);
            _estimatedHitMarker.transform.position = GetEstimatedHitPosition(point, hitedBall);
        }
        
        public Vector2 GetEstimatedHitPosition(Vector2 point, Ball hitedBall)
        {
            
            Vector3[] possiblePositions = NeighboursPositionsOf(hitedBall.transform.position);
            Vector3[] unoccupiedPositions = 
                possiblePositions.Where(pos => IsThereIsBallAt(pos) == false).ToArray();

            Vector3 estimatedHitPosition = 
                unoccupiedPositions.OrderBy(p => Vector3.Distance(p, point)).First();
            
            return estimatedHitPosition;
        }

        private Vector3[] NeighboursPositionsOf(Vector3 ballPosition)
        {
            Vector3[] positions = new Vector3[6];
            for (int i = 0; i < _neighborsOffsets.Length; i++)
            {
                positions[i] = ballPosition + _neighborsOffsets[i];
            }
            return positions;
        }
        public Ball[] NeighboursOf(Ball ball)
        {
            Ball[] neighbours = new Ball[6];
            var neighboursPositions = NeighboursPositionsOf(ball.transform.position);
            for (var n = 0; n < neighboursPositions.Length; n++)
            {
                if (IsThereIsBallAt(neighboursPositions[n]))
                {
                    neighbours[n] = GetBallAt(neighboursPositions[n]);
                }
            }

            return neighbours;
        }

        public void AffectBy(Ball addedBall)
        {
            StartCoroutine(DestroySimilarBallsUnion(addedBall));
        }
        
        private IEnumerator DestroySimilarBallsUnion(Ball addedBall)
        {
            List<Wave> similarBallsUnion = FindSimilarBallsUnion(addedBall);

            foreach (var wave in similarBallsUnion)
            {
                yield return new WaitForSeconds(0.3f);
                foreach (var sameColorBall in wave.List)
                {
                    SafeDestroy(sameColorBall);
                    Debug.Log(similarBallsUnion.Count + " " + wave.List.Count);
                }
            }
        }

       [Serializable]
       public class Wave
       {
           public List<Ball> List = new();

           public Wave(List<Ball> list)
           {
               List = list;
           }

           public Wave()
           {
               
           }
       }
       
        private List<Wave> FindSimilarBallsUnion(Ball addedBall)
        {
            List<Wave> openList = new List<Wave>();
            openList.Add(new(addedBall.Neighbours.ToList()));
            openList[0].List = openList[0].List.FindAll(b => b != null && b.Color == addedBall.Color);
            int neighboursToCheck = openList.Count;

            while (neighboursToCheck > 0)
            {
                neighboursToCheck = 0;
                foreach (Wave ballList in openList.ToList())
                {
                    foreach (Ball ball in ballList.List.ToList())
                    {
                        if (ball == null) continue;

                        var newOpenList = SameNeighboursFor(ball, addedBall.Color, openList);
                        if (newOpenList.List.Count > 0)
                            openList.Add(newOpenList);
                        neighboursToCheck += newOpenList.List.Count;
                    }
                }
            }

            return openList;
        }

        private void SafeDestroy(Ball ball)
        {
            _balls.Remove(ball);
            Destroy(ball.gameObject);
        }

        private Wave SameNeighboursFor(Ball ball, BallColor addedBallColor, List<Wave> openList)
        {
            var newOpenList = new Wave();
            foreach (Ball neighbour in ball.Neighbours)
            {
                if (neighbour != null && openList.Exists(wave=>wave.List.Contains(neighbour)) == false && neighbour.Color == addedBallColor)
                {
                    newOpenList.List.Add(neighbour);
                    neighbour.GetComponent<SpriteRenderer>().color = Color.grey;
                    //RecursiveWavefrontSearch(neighbour, openList, closedList);
                }
            }

            return newOpenList;
        }

        public void AttachAndLinkNeighbours(Ball ball)
        {
            _balls.Add(ball);
            ball.Neighbours = NeighboursOf(ball);
            foreach (Ball neighbour in ball.Neighbours)
            {
                if(neighbour)
                    neighbour.Neighbours = NeighboursOf(neighbour);
            }
        }
    }
}