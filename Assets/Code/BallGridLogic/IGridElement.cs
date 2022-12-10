using UnityEngine;

namespace Code.BallGridLogic
{
    public interface IGridElement<T>
    {        
        public Vector3 Position { get;}
        T[] Neighbours { get; set; }
    }
}