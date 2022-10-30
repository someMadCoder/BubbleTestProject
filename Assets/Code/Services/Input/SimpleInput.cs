using System;
using UnityEngine;

namespace Code.Services.Input
{
    public static class SimpleInput
    {
        public static Action<Vector2> OnPointerDown { get; set; }
        public static Action<Vector2> OnPointerUp { get; set; }
        public static Action<Vector2> OnDrag  { get; set; }
    }
}