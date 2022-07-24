
using UnityEngine;

namespace StardropTools.Audio
{
    /// <summary>
    /// List of audios to cycle or randomize through, with different pitch
    /// </summary>
    [CreateAssetMenu(menuName = "Stardrop / Audio / Audio Group")]
    public class AudioGroupSO : ScriptableObject
    {
        [SerializeField] System.Collections.Generic.List<AudioClip> clips;
        [SerializeField] float minPitch = 1;
        [SerializeField] float maxPitch = 1.2f;
        [Space]
        [SerializeField] bool clearClips;

        public System.Collections.Generic.List<AudioClip> Clips { get => clips; }

        public float MinPitch { get => minPitch; }
        public float MaxPitch { get => maxPitch; }

        public float RandomPitch { get => GetRandomPitch(); }
        public AudioClip RandomClip { get => GetRandomClip(); }

        public void AddClip(AudioClip clip)
        {
            if (clips == null)
                clips = new System.Collections.Generic.List<AudioClip>();

            if (clips.Contains(clip) == false)
                clips.Add(clip);
        }

        public void SetClips(AudioClip[] clips)
        {
            if (this.clips == null)
                this.clips = new System.Collections.Generic.List<AudioClip>();

            for (int i = 0; i < clips.Length; i++)
                if (this.clips.Contains(clips[i]) == false)
                    this.clips.Add(clips[i]);
        }

        public AudioClip GetRandomClip() => clips[Random.Range(0, clips.Count)];

        public AudioClip GetClipAtIndex(int index) => clips[index];

        public float GetRandomPitch() => Random.Range(minPitch, maxPitch);

        protected virtual void OnValidate()
        {
            if (clearClips)
            {
                clips = new System.Collections.Generic.List<AudioClip>();
                clearClips = false;
            }
        }
    }
}