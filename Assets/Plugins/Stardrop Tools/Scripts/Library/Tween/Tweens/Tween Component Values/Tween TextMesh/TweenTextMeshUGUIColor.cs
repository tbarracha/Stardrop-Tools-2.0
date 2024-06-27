
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenTextMeshUGUIColor : TweenColorTargets<TextMeshProUGUI>
    {
        public TweenTextMeshUGUIColor(Color endValue, TextMeshProUGUI target) : base(endValue, target)
        {
        }

        public TweenTextMeshUGUIColor(Color endValue, params TextMeshProUGUI[] targets) : base(endValue, targets)
        {
        }

        public TweenTextMeshUGUIColor(Color endValue, List<TextMeshProUGUI> targets) : base(endValue, targets)
        {
        }

        public TweenTextMeshUGUIColor(Color startValue, Color endValue, TextMeshProUGUI target) : base(startValue, endValue, target)
        {
        }

        public TweenTextMeshUGUIColor(Color startValue, Color endValue, params TextMeshProUGUI[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenTextMeshUGUIColor(Color startValue, Color endValue, List<TextMeshProUGUI> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.TextMeshUGUIColor;
        }

        protected override void OnTweenValueUpdate(Color lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.color = lerped;
            }
        }

        protected override void GetStartValue(TextMeshProUGUI target)
        {
            StartValue = target.color;
        }
    }
}
