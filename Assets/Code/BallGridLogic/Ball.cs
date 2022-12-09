using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Code.BallGridLogic
{
    public class Ball : MonoBehaviour
    {
        [field:SerializeField] public BallColor Color { get; set;}
        public Ball[] Neighbours = new Ball[6];
        public BallGrid Grid { get; set; }

        public void MoveAndAttachToGrid(Vector3[] wayPoints, float speed, BallGrid grid, Action onReach = null)
        {
            transform.DOPath(wayPoints, PathLength(wayPoints)/speed).OnComplete(()=>OnMoveEnded(onReach, grid)).
                SetLink(gameObject);
        }

        private void OnMoveEnded(Action onReach, BallGrid grid)
        {
            AttachToGrid(grid);
            Grid.AffectBy(this);
            onReach?.Invoke();
        }

        private void AttachToGrid(BallGrid grid)
        {
            Neighbours = grid.NeighboursOf(this);
            Grid = grid;
            Grid.AttachAndLinkNeighbours(this);
        }

        private float PathLength(Vector3[] wayPoints)
        {
            Vector3 length;
            if (wayPoints.Length < 2)
            {
                length = wayPoints[0] - transform.position;
            }
            else
            {
                length = wayPoints.Aggregate((a, b) => b - a);
            }
        
            return length.magnitude;
        }

        private void OnValidate()
        {
            //TODO: change sprite color 
        }
    }
}