using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleOtherToggles : UIToggleButtonComponent
    {
        [SerializeField] bool makeSureOneRemainsTrue = false;
        [Space]
        [SerializeField] UIToggleButton[] toggles;
        [Range(0, 10)][SerializeField] int parentIterations = 5;

        public override void SetToggle(bool value)
        {
            if (value == true)
            {
                for (int i = 0; i < toggles.Length; i++)
                    toggles[i].Toggle(false, true);
            }

            else
            {
                if (makeSureOneRemainsTrue == false)
                    return;

                bool areOthersFalse = true;

                foreach (var toggle in toggles)
                {
                    if (toggle.Value == true)
                    {
                        areOthersFalse = false;
                        break;
                    }
                }

                if (areOthersFalse)
                    targetToggleButton.Toggle(true, false);
            }
        }

        [NaughtyAttributes.Button("Get Other Toggles")]
        public void GetOtherToggles()
        {
            if (targetToggleButton == null)
                return;

            Transform parent = transform.parent.parent;
            List<UIToggleButton> toggleList = new List<UIToggleButton>();

            for (int i = 0; i < parentIterations; i++)
            {
                toggleList = Utilities.GetComponentListInChildren<UIToggleButton>(parent);

                if (toggleList.Exists() == false)
                    parent = parent.parent != null ? parent.parent : parent;
                else
                    break;
            }

            // remove this toggle from the list
            for (int i = 0; i < toggleList.Count; i++)
            {
                if (toggleList[i] == targetToggleButton)
                    toggleList.Remove(toggleList[i]);
            }

            toggles = toggleList.ToArray();
        }
    }
}