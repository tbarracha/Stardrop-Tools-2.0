
using UnityEngine;
using StardropTools.Tween;

namespace StardropTools.UI
{
    public class UIToggleText : UIToggleButtonComponent
    {
        public TMPro.TextMeshProUGUI textMesh;
        [Tooltip("0-false, 1-true")]
        public string[] colors = { "False", "True" };

        public override void SetToggle(bool value)
        {
            int index = Utilities.ConvertBoolToInt(value);
            textMesh.text = colors[index];
        }
    }
}