using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
