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
        private readonly Vector3[] _neighborsOffsets;

        public int ElementsCount => _elements.Count;



        private List<IGridElement<T>> _elements;

        private int _elementsCount = 100;

        private readonly Vector3 _ballSize;

        private readonly float _bottomPositionLimit;

        private readonly float _topPositionLimit;

        public readonly float YOffset = 1.25f;
        public readonly float XOffset = 1.5f;

        private const float Tolerance = 0.1f;

        public Grid(int elementsCount, Vector3 ballSize, float startLowestElementPosition, float bottomPositionLimit, float height)
        {
            _elementsCount = elementsCount;
            _neighborsOffsets = new Vector3[] {
                new Vector2(XOffset/2, YOffset),
                new Vector2(XOffset, 0f),
                new Vector2(XOffset/2, -YOffset),
                new Vector2(-XOffset/2, -YOffset),
                new Vector2(-XOffset, 0f),
                new Vector2(-XOffset/2, YOffset),
            };
            _elements = new List<IGridElement<T>>(_elementsCount);
            _ballSize = ballSize;
            _bottomPositionLimit = bottomPositionLimit;
            _topPositionLimit = startLowestElementPosition + height;
        }


        public void AttachElement(IGridElement<T> element)
        {
            if (IsPositionFree(element.Position) == false)
            {
                throw new ArgumentException($"Element {element} position is incorrect! " +
                                            $"{element.Position} dont fit in the grid!");
            }
            
            _elements.Add(element);
            LinkNeighbours();
        }

        public bool IsPositionFreeAndCorrect(Vector3 position)
        {
            return PositionIsCorrect(position) && IsPositionFree(position);
        }

        private bool PositionIsCorrect(Vector2 position)
        {
            return XPositionIsCorrect(position.x) &&
                   YPositionIsCorrect(position.y);
        }

        public bool IsPositionFree(Vector2 position)
        {
            return !_elements.Any(b => Math.Abs(b.Position.x - position.x) < Tolerance && 
                                       Math.Abs(b.Position.y - position.y) < Tolerance);//TODO: fix
        }

        public bool XPositionIsCorrect(float xPosition)
        {
            return xPosition + _ballSize.x/2 < WorldCameraBorders.Right(Camera.main) + Tolerance &&
                   xPosition - _ballSize.x/2 > WorldCameraBorders.Left(Camera.main) - Tolerance;
        }

        private bool YPositionIsCorrect(float yPosition)
        {
            return yPosition >= _bottomPositionLimit &&
                   (_topPositionLimit + Tolerance) >= yPosition;
        }

        private void LinkNeighbours()
        {
            foreach (var gridElement in _elements)
            {
                for (var n = 0; n < 6; n++)
                {
                    gridElement.Neighbours[n] = GetElementByPositionOrReturnNull(gridElement.Position + _neighborsOffsets[n]);
                }
            }
        }

        public T GetElementByPositionOrReturnNull(Vector2 position)
        {
            IGridElement<T> gridElement = _elements.FirstOrDefault(b => Math.Abs(b.Position.x - position.x) < Tolerance &&//TODO: fix
                                                               Math.Abs(b.Position.y - position.y) < Tolerance);
            return gridElement is T ? (T)gridElement : default;
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
            AttachElement(_element);
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

        public Vector3 GetNeighbourPosition(Vector3 ballPosition, int neighbourNumber)
        {
            if (neighbourNumber > _neighborsOffsets.Length)
                throw new ArgumentException("neighbourNumber > _neighborsOffsets.Length!");
            
            return ballPosition + _neighborsOffsets[neighbourNumber];
        }
    }
}
