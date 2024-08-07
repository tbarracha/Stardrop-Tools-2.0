﻿#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// The BaseComponent abstract class is designed as a foundation for Unity game object components, implementing several key interfaces and providing methods for precise initialization, updates, and activation control.
    /// <para> This class inherits from the MonoBehaviour class and is meant to be extended by other components to streamline common functionalities.</para>
    /// </summary>
#if UNITY_EDITOR
    [CanEditMultipleObjects]
#endif
    public abstract class BaseComponent : MonoBehaviour, IInitializeable, ILateInitializeable, IActivateable, IEnabable, IUpdateable
    {
        [Header("Initialization")]
        [SerializeField] protected InitializeComponentIn initializeAt;
#if UNITY_EDITOR
        [SerializeField] protected bool debugUpdateStartEnd;
#endif

        [NaughtyAttributes.Foldout("Object Info")]
        [NaughtyAttributes.ReadOnly]
        [SerializeField] protected GameObject thisObject;

        public bool IsInitialized       { get; protected set; }
        public bool IsLateInitialized   { get; protected set; }
        public bool IsUpdating          { get; protected set; }
        public bool IsActive            { get; protected set; }

        public GameObject GameObject
        {
            get
            {
                if (thisObject == null)
                    thisObject = gameObject;

                return thisObject;
            }
        }


        protected virtual void Awake()
        {
            if (initializeAt == InitializeComponentIn.Awake)
                Initialize();
        }

        protected virtual void Start()
        {
            if (initializeAt == InitializeComponentIn.Start)
                Initialize();
        }



        /// <summary>
        /// Instead of class initializing at Start() by itself,
        /// we call this function when we want it to "Start"
        /// </summary>
        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
        }

        public virtual void LateInitialize()
        {
            if (IsLateInitialized)
                return;

            IsLateInitialized = true;
        }


        /// <summary>
        /// Set the game object's active state
        /// </summary>
        public void SetActive(bool value)
        {
            if (isActiveAndEnabled == value)
                return;

            thisObject.SetActive(value);

            if (value)
                OnActivated();
            else
                OnDeactivated();
        }

        public void Activate() => SetActive(true);

        public void Deactivate() => SetActive(false);

        protected virtual void OnActivated() { }
        protected virtual void OnDeactivated() { }



        /// <summary>
        /// Set the Component's enabled state
        /// </summary>
        public void SetEnabled(bool value)
        {
            enabled = value;
        }

        public void Enable() => SetEnabled(true);

        public void Disable() => SetEnabled(false);

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }



        public virtual void StartUpdate()
        {
            if (IsUpdating)
                return;
#if UNITY_EDITOR
            if (debugUpdateStartEnd)
                Debug.Log(name + ", started Updating");
#endif
            LoopManager.AddToUpdate(this);
            IsUpdating = true;
        }

        public virtual void StopUpdate()
        {
            if (!IsUpdating)
                return;

#if UNITY_EDITOR
            if (debugUpdateStartEnd)
                Debug.Log(name + ", stopped Updating");
#endif
            
            LoopManager.RemoveFromUpdate(this);
            IsUpdating = false;
        }

        public virtual void HandleUpdate() { }


        public virtual void ResetComponentPublic()  { }

        protected virtual void ResetComponent()     { }


        protected virtual void OnDestroy()
        {
            StopUpdate();
        }

        protected virtual void OnValidate()
        {
            if (thisObject == null)
                thisObject = gameObject;
        }
    }
}