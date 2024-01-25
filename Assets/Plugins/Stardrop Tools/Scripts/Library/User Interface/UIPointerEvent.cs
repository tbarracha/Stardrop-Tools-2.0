
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

        public readonly CustomEvent OnPointerDownEvent = new CustomEvent();
        public readonly CustomEvent OnPointerUpEvent = new CustomEvent();

        public readonly CustomEvent OnPointerEnterEvent = new CustomEvent();
        public readonly CustomEvent OnPointerExitEvent = new CustomEvent();

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