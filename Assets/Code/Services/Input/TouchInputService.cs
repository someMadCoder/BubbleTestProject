using System;
using UnityEngine.EventSystems;

namespace Code.Services.Input
{
    class TouchInputService : IInputService
    {
        public Action OnStartDrag
        {
            get => SimpleInput.OnStartDrag;
            set => SimpleInput.OnStartDrag = value;
        }

        public Action<PointerEventData> OnDrag
        {
            get => SimpleInput.OnDrag;
            set => SimpleInput.OnDrag = value;
        }

        public Action OnEndDrag
        {
            get => SimpleInput.OnEndDrag;
            set => SimpleInput.OnEndDrag= value;
        }
    }
}