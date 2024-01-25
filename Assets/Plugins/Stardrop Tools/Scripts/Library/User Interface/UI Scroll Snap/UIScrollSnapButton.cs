
namespace StardropTools.UI
{
    public class UIScrollSnapButton : UIToggleButton
    {
        [UnityEngine.SerializeField] private UIScrollSnap scrollSnap;

        internal void SetScrollSnap(UIScrollSnap uiScrollSnap) => scrollSnap = uiScrollSnap;

        public override void Toggle()
        {
            if (ButtonID == scrollSnap.currentIndex)
                return;

            base.Toggle();
        }
    }
}