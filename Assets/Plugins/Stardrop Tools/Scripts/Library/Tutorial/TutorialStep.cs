
using UnityEngine;

namespace StardropTools.Tutorial
{
    public abstract class TutorialStep : MonoBehaviour
    {
        public int id = 0;
        public bool isLastStep = false;

        public abstract void StepStart();
        public abstract void StepUpdate();

        public virtual void StepComplete()
        {
            TutorialManager.OnTutorialStepComplete?.Invoke(id);
        }
    }
}