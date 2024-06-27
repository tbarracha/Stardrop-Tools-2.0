
using System.Linq;
using UnityEngine;
using StardropTools.Animation;
using NaughtyAttributes;
using UnityEditor;


#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

namespace StardropTools
{
    public class AnimatorControllerManager : MonoBehaviour
    {
        [Header("Animation Components")]
        [SerializeField] private Animator animator;
        [SerializeField] protected AnimationEventListener animEventListener;
        [SerializeField] bool debug;

        [Header("Animator States")]
        [SerializeField] private AnimatorControllerStateInfo currentState;
        [SerializeField] private AnimatorControllerStateInfo[] animationStates;
        [Min(0)][SerializeField] protected float overralCrossFadeTime = 0;
        [Space]
        [SerializeField] bool getOrCreateAnimationEventListener = true;

        [Header("State Transition")]
        [Foldout("State Transition")]
        [SerializeField] bool createAnyStateTransitions;
        [Foldout("State Transition")]
        [SerializeField] bool clearAnyStateTransitions;
        [Foldout("State Transition")]
        [SerializeField] bool clearTriggerParameters;

        private Timer triggerResetTimer;

        public AnimationEventListener AnimationEventListener => animEventListener != null ? animEventListener : null;

        // API
        // ========================================================================================

        // » Play Animations
        // ----------------------------------------------------------------------------------------

        public bool PlayAnimation(int animationStateIndex)
        {
            if (!animationStates.IsIndexWithinBounds(animationStateIndex))
            {
                Debug.Log($"No anim state with index: {animationStateIndex}");
                return false;
            }

            return PlayAnimation(animationStates[animationStateIndex]);
        }

        public bool PlayAnimation(string animationStateName)
        {
            foreach (AnimatorControllerStateInfo state in animationStates)
            {
                if (state.StateName == animationStateName)
                {
                    return PlayAnimation(state);
                }
            }

            Debug.Log($"Didn't find any state with name: {animationStateName}");
            return false;
        }

        public bool PlayAnimation(AnimatorControllerStateInfo targetAnimationState)
        {
            return ChangeAnimationState(targetAnimationState);
        }



        // » Setters
        // ----------------------------------------------------------------------------------------

        public void SetAnimatorController(RuntimeAnimatorController runtimeAnimator)
        {
            if (animator.runtimeAnimatorController == runtimeAnimator)
                return;

            animator.runtimeAnimatorController = runtimeAnimator;
        }

        public void SetAnimatorParameter(AnimatorControllerParameterType parameterType, string paramName, string parameterValue)
        {
            switch (parameterType)
            {
                case AnimatorControllerParameterType.Bool:
                    SetAnimatorBool(paramName, bool.Parse(parameterValue));
                    break;

                case AnimatorControllerParameterType.Int:
                    SetAnimatorInt(paramName, int.Parse(parameterValue));
                    break;

                case AnimatorControllerParameterType.Float:
                    SetAnimatorFloat(paramName, float.Parse(parameterValue));
                    break;

                case AnimatorControllerParameterType.Trigger:
                    if (int.TryParse(parameterValue, out int triggerID))
                    {
                        SetAnimatorTrigger(triggerID);
                    }
                    else
                    {
                        SetAnimatorTrigger(paramName);
                    }
                    break;
            }
        }

        public void SetAnimatorBool(string boolName, bool value)
        {
            animator.SetBool(boolName, value);
        }

        public void SetAnimatorInt(string intName, int value)
        {
            animator.SetInteger(intName, value);
        }

        public void SetAnimatorFloat(string floatName, float value)
        {
            animator.SetFloat(floatName, value);
        }

        public void SetAnimatorTrigger(string triggerName, float resetTime = 0)
        {
            animator.SetTrigger(triggerName);

            if (resetTime > 0)
            {
                triggerResetTimer?.Stop();
                triggerResetTimer = new Timer(resetTime).Play(() => ResetTrigger(triggerName));
            }
        }

        public void SetAnimatorTrigger(int triggerID, float resetTime = 0)
        {
            animator.SetTrigger(triggerID);

            if (resetTime > 0)
            {
                triggerResetTimer?.Stop();
                triggerResetTimer = new Timer(resetTime).Play(() => ResetTrigger(triggerID));
            }
        }

        public void ResetTrigger(string trigger)
        {
            animator.ResetTrigger(trigger);
        }

        public void ResetTrigger(int trigger)
        {
            animator.ResetTrigger(trigger);
        }

        public void ResetTrigger(AnimatorControllerStateInfo stateInfo)
        {
            ResetTrigger(stateInfo.Trigger);
        }

        public void SetCurrentAnimationTime(float time)
        {
            float targetTime = time * currentState.LengthInSeconds;
            animator.Play(currentState.StateHash, 0, targetTime);
        }




        // Internal
        // ========================================================================================

        private bool ChangeAnimationState(int animationStateIndex)
        {
            if (!animationStates.IsIndexWithinBounds(animationStateIndex))
            {
                Debug.Log($"No anim state with index: {animationStateIndex}");
                return false;
            }

            return ChangeAnimationState(animationStates[animationStateIndex]);
        }

        private bool ChangeAnimationState(AnimatorControllerStateInfo targetState)
        {
            if (IsTargetStateCurrentState(targetState))
            {
                return false;
            }
            else
            {
                SetCurrentState(targetState);
            }

            switch (targetState.StateChangeType)
            {
                case AnimatorControllerStateChangeType.Play:
                    return PlayAnimationState(targetState);

                case AnimatorControllerStateChangeType.CrossFade:
                    return CrossFadeAnimationState(targetState);

                case AnimatorControllerStateChangeType.Trigger:
                    return TriggerAnimationState(targetState);
            }

            return false;
        }

        private bool PlayAnimationState(AnimatorControllerStateInfo animStateInfo)
        {
            if (animStateInfo.StateChangeType != AnimatorControllerStateChangeType.Play)
            {
                Debug.Log($"State with name '{animStateInfo.StateName}' cannot be PLAYED.");
                return false;
            }

            animator.Play(animStateInfo.StateHash);
            return true;
        }

        private bool CrossFadeAnimationState(AnimatorControllerStateInfo animStateInfo)
        {
            if (animStateInfo.StateChangeType != AnimatorControllerStateChangeType.CrossFade)
            {
                Debug.Log($"State with name '{animStateInfo.StateName}' cannot be CROSSFADED.");
                return false;
            }

            animator.CrossFade(animStateInfo.StateHash, animStateInfo.Crossfade);
            return true;
        }

        private bool TriggerAnimationState(AnimatorControllerStateInfo animStateInfo)
        {
            if (animStateInfo.StateChangeType != AnimatorControllerStateChangeType.Trigger)
            {
                Debug.Log($"State with name '{animStateInfo.StateName}' cannot be TRIGGERED.");
                return false;
            }

            animator.SetTrigger(animStateInfo.StateHash);
            return true;
        }


        private void SetCurrentState(AnimatorControllerStateInfo targetState)
        {
            if (!IsTargetStateCurrentState(targetState))
            {
                if (debug)
                    Debug.Log($"Anim Changed from: '{currentState.StateName}', to: '{targetState.StateName}'");

                this.currentState = targetState;
            }
        }

        private bool IsTargetStateCurrentState(AnimatorControllerStateInfo targetState)
        {
            bool result = currentState.StateHash == targetState.StateHash;

            if (debug && result)
                Debug.Log($"Target state: '{targetState.StateName}', is the current state!");

            return result;
        }



        [Button("Get Animator")]
        public void GetAnimator()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
                if (animator == null)
                    animator = GetComponentInChildren<Animator>();
            }

            if (getOrCreateAnimationEventListener && animator != null && animEventListener == null)
            {
                animEventListener = animator.GetComponent<AnimationEventListener>();

                if (animEventListener == null)
                    animEventListener = animator.gameObject.AddComponent<AnimationEventListener>();
            }
        }

#if UNITY_EDITOR

        [Button("Create Animation States")]
        public void GetAllAnimatorStates()
        {
            if (animator == null)
            {
                GetAnimator();

                if (animator == null)
                {
                    Debug.LogError("Animator not found!");
                    return;
                }
            }

            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
            if (animatorController == null)
            {
                Debug.LogError("AnimatorController not found!");
                return;
            }

            AnimatorStateMachine stateMachine = animatorController.layers[0].stateMachine;
            AnimatorState[] states = stateMachine.states.Select(state => state.state).ToArray();

            animationStates = new AnimatorControllerStateInfo[states.Length];

            for (int i = 0; i < states.Length; i++)
            {
                AnimatorState state = states[i];
                AnimatorControllerStateInfo stateInfo = new AnimatorControllerStateInfo(
                    state.name,
                    state.nameHash,
                    0, // Layer index, update this if needed
                    state.motion != null ? state.motion.averageDuration : 0, // Length in seconds
                    AnimatorControllerStateChangeType.CrossFade, // Default state change type
                    0.15f, // Default crossfade time
                    "", // Default trigger
                    0.1f
                );

                animationStates[i] = stateInfo;
            }
        }

        public void RefreshAnimStateNames()
        {
            for (int i = 0; i < animationStates.Length; i++)
            {
                var state = animationStates[i];

                string[] parts = state.StateName.Trim().Split('-');
                if (parts.Length > 0)
                {
                    state.StateName = $"{i} - {parts.GetLast().Trim()}";
                }
                else
                {
                    state.StateName = $"{i} - {state.StateName}";
                }

                animationStates[i] = state;
            }
        }

        private void OnValidate()
        {
            RefreshAnimStateNames();
        }
#endif
    }
}