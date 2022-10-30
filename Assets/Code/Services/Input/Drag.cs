using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services.Input
{
    [RequireComponent(typeof(EventTrigger))]
    public class Drag : MonoBehaviour
    {
        [SerializeField] private IInputService _inputService;
        private void Awake()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            
            EventTrigger.Entry onPointerDownEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            onPointerDownEntry.callback.AddListener((data) => OnPointerDown((PointerEventData)data));
            
            EventTrigger.Entry onDragEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Drag
            };
            onDragEntry.callback.AddListener((data) => OnDrag((PointerEventData)data));
            
            EventTrigger.Entry onPointerUpEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            onPointerUpEntry.callback.AddListener((data) => OnPointerUp((PointerEventData)data));
            
            trigger.triggers.Add(onPointerDownEntry);
            trigger.triggers.Add(onDragEntry);
            trigger.triggers.Add(onPointerUpEntry);
        }
            
        private void OnPointerDown(PointerEventData eventData)
        {
            SimpleInput.OnPointerDown?.Invoke(UnityEngine.Input.mousePosition);
        }

        private void OnDrag(PointerEventData eventData)
        {
            SimpleInput.OnDrag?.Invoke(UnityEngine.Input.mousePosition);

        }

        private void OnPointerUp(PointerEventData eventData)
        {
            SimpleInput.OnPointerUp?.Invoke(UnityEngine.Input.mousePosition);
        }
    }
}