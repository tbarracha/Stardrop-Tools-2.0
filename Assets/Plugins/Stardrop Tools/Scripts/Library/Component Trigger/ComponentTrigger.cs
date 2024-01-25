
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ComponentTrigger : MonoBehaviour, ITriggable
    {
        [SerializeField] protected List<ITriggable> triggables = new List<ITriggable>();

        public void TriggerBehaviour()
        {
            foreach (ITriggable triggable in triggables)
            {
                triggable.TriggerBehaviour();
            }
        }

        public bool AddTriggable(ITriggable triggable)
        {
            if (triggables.Contains(triggable))
                return false;

            triggables.Add(triggable);
            return true;
        }

        public bool RemoveTriggable(ITriggable triggable)
        {
            if (!triggables.Contains(triggable))
                return false;

            triggables.Remove(triggable);
            return true;
        }
    }
}