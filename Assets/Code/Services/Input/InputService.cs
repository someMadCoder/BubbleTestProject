using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services.Input
{
    public class InputService : IInputService
    {
        public Action<Vector2> OnPointerDown
        {
            get => SimpleInput.OnPointerDown;
            set => SimpleInput.OnPointerDown = value;
        }

        public Action<Vector2> OnDrag
        {
            get =>SimpleInput.OnDrag;
            set => SimpleInput.OnDrag = value;
        }

        public Action<Vector2> OnPointerUp
        {
            get => SimpleInput.OnPointerUp;
            set => SimpleInput.OnPointerUp= value;
        }
    }
}