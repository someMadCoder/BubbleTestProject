using Unity.VisualScripting;
using UnityEngine;

namespace Code.BallGridLogic
{
    public interface IBall: IGridElement<IBall>
    {
        public BallColor Color { get; }
        public GameObject gameObject { get; }
    }
}