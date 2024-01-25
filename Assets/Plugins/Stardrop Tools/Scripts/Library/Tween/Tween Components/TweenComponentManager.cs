
using System.Linq;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenComponentManager : MonoBehaviour
    {
        [SerializeField] TweenComponent[] tweens;
        [SerializeField] TweenComponentManager[] nextManagers;
        [SerializeField] float nextManagerDelay = 0;

        [Header("Randomize")]
        [SerializeField] TweenComponentDurationAndDelayRandomizer[] randomizers;

        TweenComponent longestTween;
        Timer timeableAction;

        System.Action OnPlayCompleteCallback;

        public readonly CustomEvent OnTweenComplete = new CustomEvent();

        private void Start()
        {
            FindLongestTween();
        }

        [NaughtyAttributes.Button("Get Tweens")]
        public void GetTweens()
        {
            var selfTweens  = GetComponents<TweenComponent>();
            var childTweens = Utilities.GetComponentListInChildren<TweenComponent>(transform);

            tweens = selfTweens.Concat(childTweens).ToArray();
        }
        
        [NaughtyAttributes.Button("Start Tweens")]
        public void Play()
        {
            if (tweens.Exists() == false)
            {
                print("No tweens to play!");
                return;
            }

            ApplyRandomizers();

            for (int i = 0; i < tweens.Length; i++)
                tweens[i].Play();

            StartTimeableAction();
        }

        public void Play(System.Action onPlayFinishedCallback)
        {
            Play();
            OnPlayCompleteCallback = onPlayFinishedCallback;
        }

        [NaughtyAttributes.Button("Stop Tweens")]
        public void Stop()
        {
            if (tweens.Exists() == false)
            {
                print("No tweens to stop!");
                return;
            }

            StopTimeableAction();

            for (int i = 0; i < tweens.Length; i++)
                tweens[i].Stop();

            for (int i = 0; i < nextManagers.Length; i++)
                nextManagers[i].Stop();
        }

        public void Pause()
        {
            for (int i = 0; i < tweens.Length; i++)
                tweens[i].Pause();

            for (int i = 0; i < nextManagers.Length; i++)
                nextManagers[i].Pause();
        }

        void PlayNextTweenManagers()
        {
            if (nextManagers.Exists() == false)
                return;

            for (int i = 0; i < nextManagers.Length; i++)
                nextManagers[i].Play();

            print("Playing next managers");
        }

        void ApplyRandomizers()
        {
            if (randomizers.Exists() == false)
                return;

            for (int i = 0; i < randomizers.Length; i++)
            {
                randomizers[i].AppyRandomDelay();
                randomizers[i].ApplyRandomDuration();
            }
        }

        void TweensComplete()
        {
            OnPlayCompleteCallback?.Invoke();
            OnPlayCompleteCallback = null;

            OnTweenComplete?.Invoke();
            PlayNextTweenManagers();
        }

        public float FindLongestTween()
        {
            if (tweens.Exists() == false)
                return 0;

            longestTween = null;

            foreach (var tween in tweens)
            {
                if (longestTween == null)
                    longestTween = tween;

                if (longestTween.duration < tween.duration)
                    longestTween = tween;
            }

            return longestTween.duration;
        }

        float GetTimerDuration()
        {
            if (nextManagerDelay > 0)
                return nextManagerDelay;
            else
            {
                if (longestTween == null)
                    FindLongestTween();

                return longestTween.duration;
            }
        }

        void StartTimeableAction()
        {
            StopTimeableAction();
            timeableAction = new Timer(GetTimerDuration()).Play(TweensComplete);
        }

        void StopTimeableAction()
        {
            timeableAction?.Stop();
        }

        private void OnValidate()
        {
            FindLongestTween();
        }
    }

    [System.Serializable]
    internal class TweenComponentDurationAndDelayRandomizer
    {
        public TweenComponent[] components;
        public float minDelay;
        public float maxDelay;
        [Space]
        public float minDuration;
        public float maxDuration;

        public float RandomDelay => Random.Range(minDelay, maxDelay);
        public float RandomDuration => Random.Range(minDuration, maxDuration);

        public void AppyRandomDelay()
        {
            for(int i = 0;i < components.Length;i++)
                components[i].delay = RandomDelay;
        }

        public void ApplyRandomDuration()
        {
            for (int i = 0; i < components.Length; i++)
                components[i].duration = RandomDuration;
        }
    }
}