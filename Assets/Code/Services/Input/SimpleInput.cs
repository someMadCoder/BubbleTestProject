using System;
using UnityEngine.EventSystems;

namespace Code.Services.Input
{
    public static class SimpleInput
    {
        public static Action OnStartDrag { get; set; }
        public static Action OnEndDrag { get; set; }
        public static Action<PointerEventData> OnDrag  { get; set; }
    }
}