using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public static AudioManager instance = null;
    AudioSource source;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
    }

    public static void Play(AudioClip clip)
    {
        if(instance!=null)instance.source.PlayOneShot(clip);
    }
    public static void Play(AudioClip clip, float volume)
    {
        if (instance != null) instance.source.PlayOneShot(clip, volume);
    }
}
