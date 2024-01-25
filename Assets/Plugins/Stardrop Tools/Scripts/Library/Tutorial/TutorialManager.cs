
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tutorial
{
    public class TutorialManager : BaseManager<TutorialManager>
    {
        public static readonly CustomEvent OnTutorialComplete = new CustomEvent();
        public static readonly CustomEvent<int> OnTutorialStepComplete = new CustomEvent<int>();

        [Header("Tutorial Steps")]
        [SerializeField] TutorialStep currentStep;
        [SerializeField] TutorialStep[] steps;

        public override void Initialize()
        {
            base.Initialize();
            OrganizeSteps();
        }

        protected override void EventSubscription()
        {
            base.EventSubscription();

            OnTutorialStepComplete.AddListener(TutorialStepCompleted);
        }

        public override void GameStateChanged(GameState newGameState, GameState previousGameState) { }

        [NaughtyAttributes.Button("Organize Steps")]
        public void OrganizeSteps()
        {
            if (steps == null || steps.Length == 0)
            {
                Debug.Log("There are no Tutorial Steps!");
                return;
            }

            List<TutorialStep> stepList = new List<TutorialStep>();

            // check for nulls
            for (int i = 0; i < steps.Length; i++)
            {
                if (steps[i] != null)
                    stepList.Add(steps[i]);
            }

            // organize
            for (int i = 0; i < stepList.Count; i++)
            {
                stepList[i].id = i;
                stepList[i].isLastStep = false;

                if (i == stepList.GetLastIndex())
                    stepList[i].isLastStep = true;
            }

            steps = stepList.ToArray();
        }

        void TutorialStepCompleted(int stepID)
        {
            if (stepID >= steps.Length)
                return;

            TutorialStep step = steps[stepID];

            if (step.isLastStep == false)
            {
                NextStep();
            }
            else
            {
                OnTutorialComplete?.Invoke();
            }
        }

        public void NextStep()
        {
            int nextIndex = currentStep.id + 1;

            if (nextIndex >= steps.Length)
            {
                Debug.Log("Something wrong with tutorial steps!");
                return;
            }

            currentStep = steps[nextIndex];
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            OnTutorialComplete.ClearAllListeners();
            OnTutorialStepComplete.ClearAllListeners();
        }

        [NaughtyAttributes.Button("Get Steps")]
        void GetSteps()
        {
            steps = Utilities.GetComponentArrayInChildren<TutorialStep>(transform);
        }
    }
}