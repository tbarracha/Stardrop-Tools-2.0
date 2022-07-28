
using UnityEngine;

namespace StardropTools.UI
{
    public class UIButton : BaseUIObject
    {
        [SerializeField] UnityEngine.UI.Button button;
        [SerializeField] UnityEngine.UI.Selectable selectable;

        public UnityEngine.Events.UnityEvent OnClick => button.onClick;

        public override void Initialize()
        {
            base.Initialize();


        }
    }
}