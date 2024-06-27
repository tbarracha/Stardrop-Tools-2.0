
using UnityEngine;
using UnityEngine.EventSystems;

namespace StardropTools.UI
{
    public class UIPointerEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] bool isPressed;
        [SerializeField] bool isPointerOnUI;

        public bool IsPressed => isPressed;
        public bool IsPointerOverUI => isPointerOnUI;

        public readonly EventCallback OnPointerDownEvent = new EventCallback();
        public readonly EventCallback OnPointerUpEvent = new EventCallback();

        public readonly EventCallback OnPointerEnterEvent = new EventCallback();
        public readonly EventCallback OnPointerExitEvent = new EventCallback();

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
            OnPointerDownEvent?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
            OnPointerUpEvent?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isPointerOnUI = true;
            OnPointerEnterEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerOnUI = false;
            OnPointerExitEvent?.Invoke();
        }
    }
}