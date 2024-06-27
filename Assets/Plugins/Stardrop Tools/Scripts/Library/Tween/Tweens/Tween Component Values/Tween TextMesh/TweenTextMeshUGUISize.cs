
using System.Collections.Generic;
using TMPro;

namespace StardropTools.Tween
{
    public class TweenTextMeshUGUISize : TweenFloatTargets<TextMeshProUGUI>
    {
        public TweenTextMeshUGUISize(float endValue, TextMeshProUGUI target) : base(endValue, target)
        {
        }

        public TweenTextMeshUGUISize(float endValue, params TextMeshProUGUI[] targets) : base(endValue, targets)
        {
        }

        public TweenTextMeshUGUISize(float endValue, List<TextMeshProUGUI> targets) : base(endValue, targets)
        {
        }

        public TweenTextMeshUGUISize(float startValue, float endValue, TextMeshProUGUI target) : base(startValue, endValue, target)
        {
        }

        public TweenTextMeshUGUISize(float startValue, float endValue, params TextMeshProUGUI[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenTextMeshUGUISize(float startValue, float endValue, List<TextMeshProUGUI> targets) : base(startValue, endValue, targets)
        {
        }

        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.TextMeshUGUISize;
        }

        protected override void OnTweenValueUpdate(float lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.fontSize = lerped;
            }
        }

        protected override void GetStartValue(TextMeshProUGUI target)
        {
            StartValue = target.fontSize;
        }
    }
}
