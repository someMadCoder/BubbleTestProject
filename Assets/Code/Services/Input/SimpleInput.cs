using System;
using UnityEngine;

namespace Code.Services.Input
{
    public static class SimpleInput
    {
        public static Action OnPointerDown { get; set; }
        public static Action<Vector2> OnDrag  { get; set; }
        public static Action OnPointerUp { get; set; }
    }
}