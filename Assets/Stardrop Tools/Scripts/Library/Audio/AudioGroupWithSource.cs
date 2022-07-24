﻿
namespace StardropTools.Audio
{
    [System.Serializable]
    public class AudioGroupWithSource
    {
        [UnityEngine.SerializeField] UnityEngine.AudioSource source;
        [UnityEngine.SerializeField] AudioGroupSO group;

        public UnityEngine.AudioSource Source { get => source; }
        public AudioGroupSO AudioDB { get => group; }
        public UnityEngine.AudioClip RandomClip { get => group.GetRandomClip(); }
        public float RandomPitch { get => group.GetRandomPitch(); }

        public UnityEngine.AudioClip GetClipAtIndex(int index) => group.GetClipAtIndex(index);

        #region Play Audio
        public void PlayRandom()
        {
            StopPlaying();
            source.clip = RandomClip;
            source.Play();
        }

        public void PlayRandomOneShot(bool randomPitch = false)
        {
            if (randomPitch)
                source.pitch = RandomPitch;

            source.PlayOneShot(RandomClip);
        }


        public void PlayClipAtIndex(int index, bool randomPitch = false)
        {
            StopPlaying();
            source.clip = group.GetClipAtIndex(index);

            if (randomPitch)
                source.pitch = RandomPitch;

            source.Play();
        }

        public void PlayClipOneShotAtIndex(int index, bool randomPitch)
        {
            if (randomPitch)
                source.pitch = RandomPitch;

            source.PlayOneShot(group.GetClipAtIndex(index));
        }

        public void StopPlaying()
        {
            if (source.isPlaying)
                source.Stop();
        }
        #endregion // Play audio

        public void SetVolume(float value) => source.volume = value;
        public void SetPitch(float value) => source.pitch = value;
    }
}