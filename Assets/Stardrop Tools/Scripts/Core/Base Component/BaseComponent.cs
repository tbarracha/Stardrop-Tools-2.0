

namespace StardropTools
{
    /// <summary>
    /// Base component from which most of Stardrop Tools scripts derive
    /// </summary>
    public class BaseComponent : UnityEngine.MonoBehaviour
    {
        [UnityEngine.Header("Base Component")]
        [UnityEngine.SerializeField] protected BaseComponentData baseData;

        protected bool StopUpdateOnDisable { get => baseData.stopUpdateOnDisable; set => baseData.stopUpdateOnDisable = value; }

        public bool IsInitialized { get; protected set; }
        public bool IsLateInitialized { get; protected set; }

        public bool IsUpdating { get; protected set; }
        public bool IsFixedUpdating { get; protected set; }
        public bool IsLateUpdating { get; protected set; }


        #region Events

        public readonly BaseEvent OnInitialize = new BaseEvent();
        public readonly BaseEvent OnLateInitialize = new BaseEvent();

        public readonly BaseEvent OnUpdate = new BaseEvent();
        public readonly BaseEvent OnFixedUpdate = new BaseEvent();
        public readonly BaseEvent OnLateUpdate = new BaseEvent();

        public readonly BaseEvent OnEnabled = new BaseEvent();
        public readonly BaseEvent OnDisabled = new BaseEvent();

        public readonly BaseEvent OnReset = new BaseEvent();

        #endregion // events

        #region Print & Debug.log
        /// <summary>
        /// substitute to Debug.Log();
        /// </summary>
        public static void Print(object message) => UnityEngine.Debug.Log(message);

        /// <summary>
        /// substitute to Debug.LogWarning();
        /// </summary>
        public static void PrintWarning(object message) => UnityEngine.Debug.LogWarning(message);
        #endregion // print


        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
            OnInitialize?.Invoke();
        }

        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
                return;

            IsLateInitialized = true;
            OnLateInitialize?.Invoke();
        }


        // Start Update
        public virtual void StartUpdate()
        {
            if (IsUpdating)
                return;

            LoopManager.OnUpdate.AddListener(UpdateLogic);
            IsUpdating = true;
        }

        public virtual void StartFixedUpdate()
        {
            if (IsFixedUpdating)
                return;

            LoopManager.OnUpdate.AddListener(FixedUpdateLogic);
            IsFixedUpdating = true;
        }

        public virtual void StartLateUpdate()
        {
            if (IsLateUpdating)
                return;

            LoopManager.OnUpdate.AddListener(LateUpdateLogic);
            IsLateUpdating = true;
        }


        // Update
        public virtual void UpdateLogic()
            => OnUpdate?.Invoke();

        public virtual void FixedUpdateLogic()
            => OnFixedUpdate?.Invoke();

        public virtual void LateUpdateLogic()
            => OnLateUpdate?.Invoke();


        // Stop Update
        public virtual void StopUpdate()
        {
            if (IsUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(UpdateLogic);
            IsUpdating = false;
        }

        public virtual void StopFixedUpdate()
        {
            if (IsFixedUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(FixedUpdateLogic);
            IsFixedUpdating = false;
        }

        public virtual void StopLateUpdate()
        {
            if (IsLateUpdating == false)
                return;

            LoopManager.OnUpdate.RemoveListener(LateUpdateLogic);
            IsLateUpdating = false;
        }

        public virtual void ResetObject()
        {
            OnReset?.Invoke();
        }


        protected virtual void Awake()
        {
            if (baseData.InitializationAt == EBaseInitialization.awake)
                Initialize();

            if (baseData.LateInitializationAt == EBaseInitialization.awake)
                LateInitialize();
        }

        protected virtual void Start()
        {
            if (baseData.InitializationAt == EBaseInitialization.start)
                Initialize();

            if (baseData.LateInitializationAt == EBaseInitialization.start)
                LateInitialize();
        }

        protected virtual void OnEnable()
        {
            OnEnabled.Invoke();
        }

        protected virtual void OnDisable()
        {
            if (StopUpdateOnDisable)
            {
                StopUpdate();
                StopFixedUpdate();
                StopLateUpdate();
            }

            OnDisabled?.Invoke();
        }

        public virtual void Reset()
        {
            IsInitialized = false;
            OnReset?.Invoke();
        }
    }
}