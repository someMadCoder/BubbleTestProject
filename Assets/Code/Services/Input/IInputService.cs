using System;
using Code.Infrastructure.Services;
using UnityEngine.EventSystems;

namespace Code.Services.Input
{
    public interface IInputService: IService
    {
        public Action OnStartDrag { get; set; }
        public Action OnEndDrag { get; set; }
        public Action<PointerEventData> OnDrag { get; set; }
    }
}