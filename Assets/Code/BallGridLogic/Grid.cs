using System;
using System.Collections.Generic;
using System.Linq;
using Code.CameraLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.BallGridLogic
{
    public class Grid<T>
    {
        private Vector3[] _neighborsOffsets =
        {
            new Vector2(0.75f, 1.25f),
            new Vector2(1.5f, 0f),
            new Vector2(0.75f, -1.25f),
            new Vector2(-0.75f, -1.25f),
            new Vector2(-1.5f, 0f),
            new Vector2(-0.75f, 1.25f),
        };
        private List<IGridElement<T>> _elements = new List<IGridElement<T>>(100);
        private int _elementsCount = 100;
        private Vector3 _ballSize;
        private Vector3 _position;
        private float _bottomPositionLimit;
        private const float Tolerance = 0.1f;

        public void AddElement(IGridElement<T> element)
        {
            if (PositionIsCorrect(element.Position) == false)
            {
                Debug.LogError("Element position is incorrect!");
                return;
            }
            
            _elements.Add(element);
        }
        private bool YPositionIsCorrect(float yPosition)
        {
            return yPosition > _bottomPositionLimit &&
                   (_position.y + Tolerance - yPosition) >= 0;
        }

        private bool XPositionIsCorrect(float xPosition)
        {
            return xPosition + _ballSize.x/2 < WorldCameraBorders.Right(Camera.main) &&
                   xPosition - _ballSize.x/2 > WorldCameraBorders.Left(Camera.main);
        }    
        
        public bool PositionIsCorrect(Vector2 position)
        {
            return XPositionIsCorrect(position.x) &&
                   YPositionIsCorrect(position.y);
        }

        public T GetElementByPositionOrReturnNull(Vector2 position)
        {
            IGridElement<T> gridElement = _elements.FirstOrDefault(b => Math.Abs(b.Position.x - position.x) < Tolerance &&//TODO: fix
                                                               Math.Abs(b.Position.y - position.y) < Tolerance);
            return gridElement is T ? (T)gridElement : default;
        }

        public bool IsPositionFree(Vector2 position)
        {
            return _elements.Any(b => Math.Abs(b.Position.x - position.x) < Tolerance && //TODO: fix
                                   Math.Abs(b.Position.y - position.y) < Tolerance);
        }

        public Vector3[] NeighboursPositionsOf(Vector3 elementPosition)
        {
            Vector3[] positions = new Vector3[6];
            for (int i = 0; i < _neighborsOffsets.Length; i++)
            {
                positions[i] = elementPosition + _neighborsOffsets[i];
            }
            return positions;
        }

        public T[] NeighboursOf(IGridElement<T> element)
        {
            T[] neighbours = new T[6];
            var neighboursPositions = NeighboursPositionsOf(element.Position);
            for (var n = 0; n < neighboursPositions.Length; n++)
            {
                if (IsPositionFree(neighboursPositions[n])==false)
                {
                    neighbours[n] = GetElementByPositionOrReturnNull(neighboursPositions[n]);
                }
            }

            return neighbours;
        }

        public void AttachAndLinkNeighbours(IGridElement<T> _element)
        {
            AddElement(_element);
            _element.Neighbours = NeighboursOf(_element);
            foreach (IGridElement<T> neighbour in _element.Neighbours)
            {
                if(neighbour!=null)
                    neighbour.Neighbours = NeighboursOf(neighbour);
            }
        }

        public void Remove(IGridElement<T> element)
        {
            _elements.Remove(element);
        }
        
    }
}