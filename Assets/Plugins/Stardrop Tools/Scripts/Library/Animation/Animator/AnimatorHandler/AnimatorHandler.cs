using StardropTools.Animation;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// The AnimatorHandler class provides methods for playing, crossfading, and triggering animations through the Unity Animator component, while supporting event-based callbacks for animation start and completion.
    /// <para>Additionally, this class features tools for creating any-state transitions directly within the Unity Editor, streamlining animation setup and control.</para>
    /// </summary>
    public class AnimatorHandler : MonoBehaviour
    {
        [Header("Simple Animation")]
        [SerializeField] protected Animator animator;
        [SerializeField] protected AnimationEventListener animEventListener;
        [SerializeField] protected AnimatorHandlerState[] animStates;
        [SerializeField] int currentAnimID;
        [Min(0)][SerializeField] protected float overralCrossFadeTime = 0;
        [Space]
        [SerializeField] bool getOrCreateAnimationEventListener = true;
        [SerializeField] bool debug;
        [Space]
        [NaughtyAttributes.Foldout("State Transition")]
        [SerializeField] bool createAnyStateTransitions;
        [NaughtyAttributes.Foldout("State Transition")]
        [SerializeField] bool clearAnyStateTransitions;
        [NaughtyAttributes.Foldout("State Transition")]
        [SerializeField] bool clearTriggerParameters;

        int nextAnimID;
        protected float animDuration;
        protected Timer animationLifetimeTimer, nextAnimationTimer;

        public int StateCount => animStates.Length;
        public int CurrentAnimID => currentAnimID;
        public AnimatorHandlerState CurrentState => animStates[currentAnimID];
        public float CurrentStateLength => CurrentState.LengthInSeconds;

        public readonly EventCallback OnAnimStart = new EventCallback();
        public readonly EventCallback OnAnimComplete = new EventCallback();

        public readonly EventCallback<int> OnAnimStartID = new EventCallback<int>();
        public readonly EventCallback<int> OnAnimCompleteID = new EventCallback<int>();

        public readonly EventCallback<AnimatorHandlerState> OnAnimStateStart = new EventCallback<AnimatorHandlerState>();
        public readonly EventCallback<AnimatorHandlerState> OnAnimStateComplete = new EventCallback<AnimatorHandlerState>();

        public EventCallback<int> OnAnimEventINT => animEventListener.OnAnimEventINT;
        public EventCallback<string> OnAnimEventSTRING => animEventListener.OnAnimEventSTRING;


        /// <summary>
        /// Set RuntimeAnimatorController. Basically set the AnimatorController you want
        /// </summary>
        public void SetAnimatorController(RuntimeAnimatorController runtimeAnimator)
        {
            if (animator.runtimeAnimatorController == runtimeAnimator)
                return;

            animator.runtimeAnimatorController = runtimeAnimator;
        }

        public void SetAnimatorBool(string boolName, bool value) => animator.SetBool(boolName, value);

        public void SetAnimatorInt(string intName, int value) => animator.SetInteger(intName, value);

        public void SetAnimatorFloat(string floatName, float value) => animator.SetFloat(floatName, value);

        public void SetAnimatorTrigger(string triggerName) => animator.SetTrigger(triggerName);


        public void SyncAnimatorWithAnimationID(AnimatorControllerStateChangeType transitionType, bool disableOnFinish = false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != CurrentState.StateHash)
            {
                if (debug)
                    Debug.LogFormat("DIFFERENT Anim hash! - current: {0}, target: {1}", animator.GetCurrentAnimatorStateInfo(0).shortNameHash, CurrentState.StateHash);

                switch (transitionType)
                {
                    case AnimatorControllerStateChangeType.Play:
                        animator.Play(CurrentState.StateHash, CurrentState.Layer);
                        break;

                    case AnimatorControllerStateChangeType.CrossFade:
                        animator.CrossFade(CurrentState.StateHash, CurrentState.Layer);
                        break;

                    case AnimatorControllerStateChangeType.Trigger:
                        animator.SetTrigger(CurrentState.StateName);
                        break;
                }

                AnimationLifetime(CurrentState.LengthInSeconds, !disableOnFinish);
            }

            else
            {
                if (debug)
                    Debug.Log("SAME Anim hash!");
            }
        }

        /// <summary>
        /// Plays the next animation after the current animtion is finished
        /// </summary>
        public void PlayQueued(int animID, int nextAnimID, AnimatorControllerStateChangeType animationType)
        {
            switch (animationType)
            {
                case AnimatorControllerStateChangeType.Play:
                    if (PlayAnimation(animID))
                    {
                        float time = animStates[nextAnimID].LengthInSeconds;
                        nextAnimationTimer = new Timer(time).Play();
                        nextAnimationTimer.OnTimerComplete.Subscribe(() => PlayAnimation(nextAnimID));
                    }
                    break;

                case AnimatorControllerStateChangeType.CrossFade:
                    if (CrossFadeAnimation(animID))
                    {
                        float time = animStates[nextAnimID].LengthInSeconds;
                        nextAnimationTimer = new Timer(time).Play();
                        nextAnimationTimer.OnTimerComplete.Subscribe(() => CrossFadeAnimation(nextAnimID));
                    }
                    break;

                case AnimatorControllerStateChangeType.Trigger:
                    if (TriggerAnimation(animID))
                    {
                        float time = animStates[nextAnimID].LengthInSeconds;
                        nextAnimationTimer = new Timer(time).Play();
                        nextAnimationTimer.OnTimerComplete.Subscribe(() => TriggerAnimation(nextAnimID));
                    }
                    break;
            }
        }

        /// <summary>
        /// Play target animation ID. Not Smooth!
        /// </summary>
        public bool PlayAnimation(int animationID, bool checkIfSameID = true, bool disableOnFinish = false)
        {
            if (checkIfSameID == true && animationID == currentAnimID)
                return false;

            if (animationID < 0 || animationID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animationID);
                return false;
            }

            StopTimers();
            var targetState = animStates[animationID];

            if (animator.enabled == false)
                animator.enabled = true;

            //animator.Play(targetState.StateName, targetState.Layer);
            animator.Play(targetState.StateHash, targetState.Layer);

            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
            currentAnimID = animationID;

            OnAnimStartID?.Invoke(currentAnimID);
            OnAnimStateStart?.Invoke(CurrentState);

            if (debug)
                Debug.Log(targetState.StateName);

            return true;
        }

        /// <summary>
        /// Smoothly Crossfade from Current Animation, to Target Animation ID
        /// </summary>
        public bool CrossFadeAnimation(int animationID, bool checkIfSameID = true, bool disableOnFinish = false)
        {
            if (checkIfSameID == true && animationID == currentAnimID)
                return false;

            if (animationID < 0 || animationID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animationID);
                return false;
            }

            StopTimers();
            var targetState = animStates[animationID];

            if (animator.enabled == false)
                animator.enabled = true;

            //animator.CrossFade(targetState.StateName, targetState.crossfade);
            animator.CrossFade(targetState.StateHash, targetState.crossfade);

            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);
            currentAnimID = animationID;

            OnAnimStart?.Invoke();
            OnAnimStartID?.Invoke(currentAnimID);
            OnAnimStateStart?.Invoke(CurrentState);

            if (debug)
                Debug.Log(targetState.StateName);

            return true;
        }


        /// <summary>
        /// Set target animation id trigger parameter as true
        /// & smoothly crossfade to target animation
        /// </summary>
        public bool TriggerAnimation(int animationID, float resetTriggerDelay = .01f, bool checkIfSameID = true, bool disableOnFinish = false)
        {
            if (checkIfSameID == true && animationID == currentAnimID)
                return false;

            if (animationID < 0 || animationID > animStates.Length)
            {
                Debug.LogFormat("Animation ID: {0}, does not exist", animationID);
                return false;
            }

            StopTimers();
            var targetState = animStates[animationID];

            // must be name instead of hash because parameter index does not always
            // match state index since we may want to make array changes in the inspector
            animator.SetTrigger(targetState.StateName);
            AnimationLifetime(targetState.LengthInSeconds, !disableOnFinish);

            currentAnimID = animationID;
            Invoke(nameof(ResetTrigger), resetTriggerDelay);

            if (debug)
                Debug.Log(targetState.StateName);

            return true;
        }

        public void ResetTrigger()
            => animator.ResetTrigger(currentAnimID);

        protected void AnimationLifetime(float time, bool disableOnFinish)
        {
            if (isActiveAndEnabled == false)
                return;

            animationLifetimeTimer = new Timer(time).Play();
            animationLifetimeTimer.OnTimerComplete.Subscribe(() => AnimationLifetimeComplete(disableOnFinish));
        }

        protected void AnimationLifetimeComplete(bool disableOnFinish)
        {
            animator.enabled = disableOnFinish;

            OnAnimComplete?.Invoke();
            OnAnimCompleteID?.Invoke(currentAnimID);
            OnAnimStateComplete?.Invoke(CurrentState);
        }

        protected void StopTimers()
        {
            if (animationLifetimeTimer != null)
                animationLifetimeTimer.Stop();

            if (nextAnimationTimer != null)
                nextAnimationTimer.Stop();
        }


        public void SetCurrentAnimationTime(float animTime)
        {
            AnimatorHandlerState targetState = animStates[currentAnimID];
            AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float targetTime = animTime * currentAnimatorStateInfo.length;
            animator.Play(targetState.StateHash, 0, targetTime);
        }



        [NaughtyAttributes.Button("Get Animator")]
        void GetAnimator()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
                if (animator == null)
                    animator = GetComponentInChildren<Animator>();
            }

            if (getOrCreateAnimationEventListener == true && animator != null && animEventListener == null)
            {
                animEventListener = animator.GetComponent<AnimationEventListener>();

                if (animEventListener == null)
                    animEventListener = animator.gameObject.AddComponent<AnimationEventListener>();
            }
        }



#if UNITY_EDITOR

        /// <summary>
        /// 
        /// 1) Get Animator Controller Reference
        /// 2) Get Animator Controller States
        /// 3) Get Animation Clips from Animator
        /// 4) Check if States.Length == AnimClips.Length
        /// 5) Loop through states
        /// 5.1) Create AnimState based on state & animClip info
        /// 
        /// </summary>
        [NaughtyAttributes.Button("Create Anim States")]
        protected void GenerateAnimStates()
        {
            if (animator == null)
            {
                Debug.Log("Animator not found!");
                return;
            }

            // 1 & 2) Get Animator Controller States
            // 3) Get Animation Clips from Animator
            UnityEditor.Animations.ChildAnimatorState[] controllerStates = AnimUtilities.GetAnimatorStates(animator, 0);
            AnimationClip[] animClips = AnimUtilities.GetAnimationClips(animator);

            // 4) Check if States.Length == AnimClips.Length
            if (controllerStates.Length != animClips.Length)
            {
                Debug.Log("States.Lenth != Animation Clips.Length");
                return;
            }

            var animStateList = new System.Collections.Generic.List<AnimatorHandlerState>();

            // 5) Loop through states
            // 5.1) Create AnimState based on state & animClip info
            for (int i = 0; i < controllerStates.Length; i++)
            {
                UnityEditor.Animations.AnimatorState controllerState = controllerStates[i].state;
                AnimatorHandlerState newState = new AnimatorHandlerState(controllerState.name, controllerState.nameHash, 0, .15f, animClips[i].length * controllerState.speed);

                animStateList.Add(newState);
                Debug.Log("State: " + controllerStates[i].state.name);
            }

            animStates = animStateList.ToArray();
            RefreshIndexName();
        }


        // Create Any State triggerable Transitions with
        // parameter conditions that have state names

        // 1) create trigger parameters based on Animator States
        // 2) create transitions with parameter names
        void CreateAnyStateTriggerTransition()
        {
            // 1) create trigger parameters based on Animator States
            AnimUtilities.CreateTriggerParametersBasedOnStates(animator, 0, true);

            // 2) create transitions with parameter names
            for (int i = 0; i < animStates.Length; i++)
            {
                string stateName = animStates[i].StateName;
                AnimUtilities.CreateAnyStateTriggerTransition(animator, 0, stateName, stateName);
            }
        }

        void ClearAnyStateTransitions()
            => AnimUtilities.ClearAnyStateTransitions(animator, 0);

        void ClearTriggerParameters()
        {
            var parameters = animator.parameters;
            var listParamToRemove = new System.Collections.Generic.List<AnimatorControllerParameter>();

            // 1) loop through all params
            // 2) check if param is a trigger && name is equal to a reference state
            // 3) remove if true
            for (int i = 0; i < parameters.Length; i++)
            {
                var p = parameters[i];

                if (p.type == AnimatorControllerParameterType.Trigger)
                    for (int j = 0; j < animStates.Length; j++)
                    {
                        if (p.name == animStates[j].StateName)
                        {
                            listParamToRemove.Add(p);
                            break;
                        }
                    }
            }

            AnimUtilities.RemoveAnimatorParameters(animator, listParamToRemove.ToArray());
        }

        protected virtual void OnValidate()
        {
            if (animator == null)
                animator = GetComponent<Animator>();

            if (createAnyStateTransitions)
            {
                CreateAnyStateTriggerTransition();
                createAnyStateTransitions = false;
            }

            if (clearAnyStateTransitions)
            {
                ClearAnyStateTransitions();
                clearAnyStateTransitions = false;
            }

            if (clearTriggerParameters)
            {
                ClearTriggerParameters();
                clearTriggerParameters = false;
            }

            if (overralCrossFadeTime > 0)
                foreach (AnimatorHandlerState animeState in animStates)
                    animeState.crossfade = overralCrossFadeTime;

            RefreshIndexName();
        }

        void RefreshIndexName()
        {
            // Refresh state name with index at the start
            if (animStates.Exists())
                for (int i = 0; i < animStates.Length; i++)
                    animStates[i].SetIndexStateName(i);
        }
#endif
    }
}

