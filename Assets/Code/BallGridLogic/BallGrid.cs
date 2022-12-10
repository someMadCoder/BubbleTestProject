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
        [SerializeField] private GameObject _estimatedHitMarker;
        private Grid<IBall> _grid;

        public void ShowEstimatedHitPosition(Vector2 point, Ball hitedBall)
        {
            _estimatedHitMarker.SetActive(true);
            _estimatedHitMarker.transform.position = GetEstimatedHitPosition(point, hitedBall);
        }
        
        public Vector2 GetEstimatedHitPosition(Vector2 point, Ball hitedBall)
        {
            
            Vector3[] possiblePositions = _grid.NeighboursPositionsOf(hitedBall.transform.position);
            Vector3[] unoccupiedPositions = 
                possiblePositions.Where(pos => _grid.IsPositionFree(pos)).ToArray();

            Vector3 estimatedHitPosition = 
                unoccupiedPositions.OrderBy(p => Vector3.Distance(p, point)).First();
            
            return estimatedHitPosition;
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
                foreach (IBall sameColorBall in wave.List)
                {
                    SafeDestroy(sameColorBall);
                    Debug.Log(similarBallsUnion.Count + " " + wave.List.Count);
                }
            }
        }

       [Serializable]
       public class Wave
       {
           public List<IBall> List = new();

           public Wave(List<IBall> list)
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
                    foreach (IBall ball in ballList.List.ToList())
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

        private void SafeDestroy(IBall ball)
        {
            _grid.Remove(ball);
            Destroy(ball.gameObject);
        }

        private Wave SameNeighboursFor(IBall ball, BallColor addedBallColor, List<Wave> openList)
        {
            var newOpenList = new Wave();
            foreach (IBall neighbour in ball.Neighbours)
            {
                if (neighbour != null && openList.Exists(wave=>wave.List.Contains(neighbour)) == false && neighbour.Color == addedBallColor)
                {
                    newOpenList.List.Add(neighbour);
                }
            }

            return newOpenList;
        }

        public void AttachAndLinkNeighbours(Ball ball)
        {
            _grid.AttachAndLinkNeighbours(ball);
        }

        public IBall[] NeighboursOf(IBall ball)
        {
            return _grid.NeighboursOf(ball);
        }
    }
}