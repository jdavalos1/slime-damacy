using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager SharedInstance;
    [SerializeField]
    private List<Sound> sounds;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SharedInstance = this;
        foreach(var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    /// <summary>
    /// Change the volume of a predetermined value. If name is empty or null then
    /// it will default to all volume
    /// </summary>
    /// <param name="value"></param>
    /// <param name="name"></param>
    public void ChangeVolume(float value, string name)
    {
        if (name == null || name.Length == 0)
        {
            foreach (var s in sounds) s.source.volume = value;
        }
        else
        {
            sounds.Find(s => s.name == name).source.volume = value;
        }
    }

    public void Play(string sound, bool loop)
    {
        Sound s = sounds.Find(s => s.name == sound);
        if (s == null) return;
        s.source.loop = loop;
        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = sounds.Find(s => s.name == sound);
        if (s == null) return;
        s.source.Stop();
    }
}
