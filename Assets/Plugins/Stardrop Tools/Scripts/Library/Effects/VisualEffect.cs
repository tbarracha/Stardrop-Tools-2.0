
using StardropTools.Pool;
using StardropTools.Tween;
using System;
using UnityEngine;

namespace StardropTools
{
    public abstract class VisualEffect : BaseTransform, IPlayableWithAction<VisualEffect>, IPoolable
    {
        [Header("Actions")]
        [SerializeField] VisualEffectAction onPlayCompleteAction;
        [SerializeField] VisualEffectAction onStopCompleteAction;
        [Space]
        [SerializeField] float lifetime;

        [Header("Tweens")]
        [SerializeField] TweenComponentManager tweensPlay;
        [SerializeField] TweenComponentManager tweensStop;

        protected Action onPlayCallback;
        protected Action onStopCallback;
        protected Action onLifetimeCallback;

        protected Timer lifetimeTimer;

        public VisualEffectAction PlayAction { get => onPlayCompleteAction; set => onPlayCompleteAction = value; }
        public VisualEffectAction StopAction { get => onStopCompleteAction; set => onStopCompleteAction = value; }
        
        public float LifeTime { get => lifetime; set => lifetime = value; }

        protected virtual void OnPlay()
        {
            SetActive(true);
            tweensStop?.Stop();
            lifetimeTimer?.Stop();
        }

        public VisualEffect Play()
        {
            OnPlay();

            if (tweensPlay != null)
            {
                tweensPlay.Play(PerformPlayAction);
            }
            else if (!CheckIfHasLifetime())
            {
                PerformPlayAction();
            }

            return this;
        }

        public VisualEffect Play(Action onPlayCallback)
        {
            this.onPlayCallback = onPlayCallback;
            Play();

            return this;
        }

        public VisualEffect Play(VisualEffectAction visualEffectAction)
        {
            OnPlay();

            if (tweensPlay != null)
            {
                tweensPlay.Play(() => PerformPlayAction(visualEffectAction));
            }
            else if (!CheckIfHasLifetime())
            {
                PerformPlayAction(visualEffectAction);
            }

            return this;
        }

        public VisualEffect Play(VisualEffectAction visualEffectAction, Action onPlayCallback)
        {
            this.onPlayCallback = onPlayCallback;
            Play(visualEffectAction);

            return this;
        }

        protected virtual void OnStop()
        {
            tweensPlay?.Stop();
            lifetimeTimer?.Stop();
        }

        public void Stop()
        {
            OnStop();

            if (tweensStop != null)
            {
                tweensStop.Play(PerformStopAction);
            }
            else
            {
                PerformStopAction();
            }
        }

        public void Stop(Action onStopCallback)
        {
            this.onStopCallback = onStopCallback;
            Stop();
        }

        public void Stop(VisualEffectAction visualEffectAction)
        {
            OnStop();

            if (tweensStop != null)
            {
                tweensStop.Play(() => PerformStopAction(visualEffectAction));
            }
            else
            {
                PerformStopAction();
            }
        }

        public void Stop(VisualEffectAction visualEffectAction, Action onStopCallback)
        {
            this.onStopCallback = onStopCallback;
            Stop(visualEffectAction);
        }

        protected bool CheckIfHasLifetime()
        {
            if (lifetime > 0)
            {
                lifetimeTimer?.Stop();
                lifetimeTimer = new Timer(lifetime).Play(Stop);

                return true;
            }

            return false;
        }

        protected void PerformPlayAction() => PerformPlayAction(onPlayCompleteAction);

        protected virtual void PerformPlayAction(VisualEffectAction action)
        {
            onPlayCallback?.Invoke();
            onPlayCallback = null;

            switch (action)
            {
                case VisualEffectAction.Nothing:
                    // Do nothing
                    break;

                case VisualEffectAction.Deactivate:
                    SetActive(false);
                    break;

                case VisualEffectAction.Despawn:
                    Despawn();
                    break;

                default:
                    // Handle unknown lifetime actions or add new cases
                    break;
            }
        }

        protected void PerformStopAction() => PerformStopAction(onStopCompleteAction);
        protected virtual void PerformStopAction(VisualEffectAction action)
        {
            onStopCallback?.Invoke();
            onStopCallback = null;

            switch (action)
            {
                case VisualEffectAction.Nothing:
                    // Do nothing
                    break;

                case VisualEffectAction.Deactivate:
                    SetActive(false);
                    break;

                case VisualEffectAction.Despawn:
                    Despawn();
                    break;

                default:
                    // Handle unknown lifetime actions or add new cases
                    break;
            }
        }



        #region Poolable
        public PoolItem PoolItem { get; protected set; }

        public void SetPoolItem(PoolItem poolItem) => this.PoolItem = poolItem;

        public void Despawn()
        {
            if (PoolItem != null)
                PoolItem.Despawn();
            else
                Destroy(thisObject);
        }

        public virtual void OnSpawn()
        {

        }

        public virtual void OnDespawn()
        {

        }

        #endregion // Poolable
    }
}