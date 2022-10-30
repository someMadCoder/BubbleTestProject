using System;
using Code.Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services.Input
{
    public interface IInputService: IService
    {
        public Action<Vector2> OnPointerDown { get; set; }
        public Action<Vector2> OnPointerUp { get; set; }
        public Action<Vector2> OnDrag { get; set; }
    }
}