using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenManager : Singleton<TweenManager>, IUpdateable
    {
        [SerializeField] int tweenCount;
        List<ITween> tweens = new List<ITween>();
        bool isUpdating;

        protected override void Awake()
        {
            base.Awake();
            tweens = new List<ITween>();
        }


        // API
        // ----------------------------------------------------------------------------------
        public void StartUpdate()
        {
            if (isUpdating)
                return;

            LoopManager.AddToUpdate(this);
            isUpdating = true;
        }

        public void StopUpdate()
        {
            if (!isUpdating)
                return;

            LoopManager.RemoveFromUpdate(this);
            isUpdating = false;
        }

        public void HandleUpdate()
        {
            UpdateTweens();
        }

        public bool ProcessTween(ITween tween)
        {
            if (!Application.isPlaying)
            {
                Debug.Log("Can't tween on Edit Mode!");
                return false;
            }

            if (tween == null)
            {
                Debug.Log("Tween is null!");
                return false;
            }

            if (tween.Duration <= 0)
            {
                Debug.Log("Tween has zero duration!");
                return false;
            }

            FilterTween(tween.TweenID, tween.TweenType);
            AddTween(tween);

            return true;
        }


        /// <summary>
        /// Remove input tween from update list
        /// </summary>
        public void RemoveTween(ITween tween)
        {
            if (tween == null)
                return;

            if (tween.IsInManagerList)
            {
                tweens.Remove(tween);
                tween.IsInManagerList = false;
            }
        }



        // internal
        // ----------------------------------------------------------------------------------
        
        /// <summary>
        /// Check if there is already a tween of this type
        /// </summary>
        void FilterTween(int tweenID, TweenType tweenType)
        {
            if (!tweens.Exists() || tweenID == -1)
                return;

            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                ITween tween = tweens[i];
                if (tween.TweenID == tweenID && tween.TweenType == tweenType)
                {
                    tween.Stop();
                    tweens.RemoveAt(i);
                }
            }
        }

        void AddTween(ITween tween)
        {
            if (tween == null)
                return;

            if (!tween.IsInManagerList)
            {
                tweens.Add(tween);
                tween.IsInManagerList = true;

                if (!isUpdating)
                    StartUpdate();
            }
        }

        void UpdateTweens()
        {
            if (tweens.Exists())
            {
                for (int i = 0; i < tweens.Count; i++)
                {
                    if (tweens.Exists() == false)
                        break;

                    tweens[i].UpdateTweenState();
                }
            }

            tweenCount = tweens.Count;
            if (tweens.Count == 0)
                StopUpdate();
        }


        


        // Static API
        // ----------------------------------------------------------------------------------
        public static void PlayTweenables(ITweenable[] tweenComponents)
        {
            if (!tweenComponents.Exists())
                return;

            for (int i = 0; i < tweenComponents.Length; i++)
                tweenComponents[i].Play();
        }

        public static void StopTweenables(ITweenable[] tweenComponents)
        {
            if (!tweenComponents.Exists())
                return;

            for (int i = 0; i < tweenComponents.Length; i++)
                tweenComponents[i].Stop();
        }
    }
}