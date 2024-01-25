
using UnityEngine;

namespace StardropTools.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceBase : BaseComponent
    {
        [SerializeField] protected AudioSource audioSource;

        public bool isPlaying       => audioSource.isPlaying;
        public bool isPaused        { get; private set; }
        public AudioClip clip       { get => audioSource.clip; set => SetAudioClip(value); }
        public float volume         { get => audioSource.volume; set => SetVolume(value); }

        public readonly CustomEvent OnPlay                  = new CustomEvent();
        public readonly CustomEvent OnPause                 = new CustomEvent();
        public readonly CustomEvent OnUnPause               = new CustomEvent();
        public readonly CustomEvent OnStop                  = new CustomEvent();
        public readonly CustomEvent OnAudioClipChanged      = new CustomEvent();
        
        public readonly CustomEvent<float> OnVolumeChanged  = new CustomEvent<float>();


        public void SetAudioClip(AudioClip clip, bool invokeEvents = true)
        {
            if (audioSource.clip == clip)
                return;

            audioSource.clip = clip;

            if (invokeEvents)
                OnAudioClipChanged?.Invoke();
        }

        public void Play()
        {
            audioSource.Play();
            OnPlay?.Invoke();
        }

        public void PlayOrUnPause()
        {
            if (isPaused)
                UnPause();
            else
                Play();
        }

        public void Pause()
        {
            if (isPaused == true)
                return;

            audioSource.Pause();
            isPaused = true;

            OnPause?.Invoke();
        }

        public void UnPause()
        {
            if (isPaused == false)
                return;

            audioSource.UnPause();
            isPaused = false;

            OnUnPause?.Invoke();
        }

        public void Stop()
        {
            if (isPlaying == false)
                return;

            audioSource.Stop();
            isPaused = false;
            OnStop?.Invoke();
        }

        void SetVolume(float volume, bool invokeEvents = true)
        {
            audioSource.volume = volume;

            if (invokeEvents)
                OnVolumeChanged?.Invoke(volume);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }
    }
}