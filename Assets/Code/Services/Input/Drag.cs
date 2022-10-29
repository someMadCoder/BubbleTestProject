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
            
            // EventTrigger.Entry onPointerDownEntry = new EventTrigger.Entry
            // {
            //     eventID = EventTriggerType.PointerDown
            // };
            // onPointerDownEntry.callback.AddListener(_ => OnPointerDown());
            
            EventTrigger.Entry onDragEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Drag
            };
            onDragEntry.callback.AddListener((data) => OnDrag((PointerEventData)data));
            
            // EventTrigger.Entry onPointerUpEntry = new EventTrigger.Entry
            // {
            //     eventID = EventTriggerType.PointerUp
            // };
            // onPointerUpEntry.callback.AddListener((_) => OnPointerUp());
            
            // trigger.triggers.Add(onPointerDownEntry);
            trigger.triggers.Add(onDragEntry);
            // trigger.triggers.Add(onPointerUpEntry);
        }
            
        private void OnPointerDown()
        {
            SimpleInput.OnPointerDown?.Invoke();
        }

        private void OnDrag(PointerEventData eventData)
        {
            SimpleInput.OnDrag?.Invoke(UnityEngine.Input.mousePosition);
            Debug.Log("работает блять");

        }

        private void OnPointerUp()
        {
            SimpleInput.OnPointerUp?.Invoke();
        }
    }
}