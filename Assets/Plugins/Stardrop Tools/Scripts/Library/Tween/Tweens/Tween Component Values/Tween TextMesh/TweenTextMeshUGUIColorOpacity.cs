
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenTextMeshUGUIColorOpacity : TweenColorOpacityTargets<TextMeshProUGUI>
    {
        public TweenTextMeshUGUIColorOpacity(Color color, float startValue, float endValue, TextMeshProUGUI target) : base(color, startValue, endValue, target)
        {
        }

        public TweenTextMeshUGUIColorOpacity(Color color, float startValue, float endValue, params TextMeshProUGUI[] targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenTextMeshUGUIColorOpacity(Color color, float startValue, float endValue, List<TextMeshProUGUI> targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenTextMeshUGUIColorOpacity(Color color, float endValue, TextMeshProUGUI target) : base(color, endValue, target)
        {
        }

        public TweenTextMeshUGUIColorOpacity(Color color, float endValue, params TextMeshProUGUI[] targets) : base(color, endValue, targets)
        {
        }

        public TweenTextMeshUGUIColorOpacity(Color color, float endValue, List<TextMeshProUGUI> targets) : base(color, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.TextMeshUGUIOpacity;
        }

        protected override void OnTweenValueUpdate(float lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.color = LerpedColor;
            }
        }

        protected override void GetStartValue(TextMeshProUGUI target)
        {
            StartValue = target.color.a;
        }
    }
}
