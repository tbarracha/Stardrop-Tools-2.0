using StardropTools;
using StardropTools.Audio;
using UnityEngine;

public class SingletonAudioManager<T> : BaseComponent where T : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;


    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }


    void SingletonInitialization()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        SingletonInitialization();
    }
    #endregion // singleton

    public void PlayOneShotAtSource(AudioSource source, AudioClip clip, float pitch = 1)
    {
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }

    public void PlayFromAudioGroup(AudioSource source, AudioGroupSO group, int index, float pitch = 1)
        => PlayOneShotAtSource(source, group.GetClipAtIndex(index), pitch);

    public void PlayRandomFromAudioGroup(AudioSource source, AudioGroupSO group, float pitch = 1)
        => PlayOneShotAtSource(source, group.GetRandomClip(), pitch);


    public void PlayFromAudioGroupWithSource(AudioGroupWithSource audioSourceDB, int clipIndex, float pitch = 1)
        => PlayOneShotAtSource(audioSourceDB.Source, audioSourceDB.GetClipAtIndex(clipIndex), pitch);

    public void PlayRandomFromAudioGroupWithSource(AudioGroupWithSource audioSourceDB, float pitch)
        => PlayOneShotAtSource(audioSourceDB.Source, audioSourceDB.RandomClip, pitch);


    public void PlaySourceClip(AudioSourceWithClip sourceClip)
    {
        sourceClip.Play();
    }

    public void PlaySourceClipOneShot(AudioSourceWithClip sourceClip)
    {
        sourceClip.Source.PlayOneShot(sourceClip.Clip);
    }
}
