
using StardropTools.Tween;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleTweenComponentManagers : UIToggleButtonComponent
    {
        [Tooltip("0-false, 1-true")]
        [SerializeField] TweenComponentManager[] tweenComponentManagers;

        public override void SetToggle(bool value)
        {
            int index = Utilities.ConvertBoolToInt(value);

            for (int i = 0; i < tweenComponentManagers.Length; i++)
            {
                if (i != index)
                    tweenComponentManagers[i].Stop();
            }

            tweenComponentManagers[index].Play();
        }
    }
}