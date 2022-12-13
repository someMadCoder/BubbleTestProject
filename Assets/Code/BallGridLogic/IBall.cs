using Unity.VisualScripting;
using UnityEngine;

namespace Code.BallGridLogic
{
    public interface IBall: IGridElement<IBall>
    {
        public BallColor Color { get; }
        public Vector2 Size { get; }
        public GameObject gameObject { get; }
        public BallGrid Grid { get; set; }
    }
}