using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;

    [Range(0.0f, 1.0f)] public float volume;

    [Range(0.1f, 3.0f)] public float pitch;
    public bool doesLoop;

    [HideInInspector] public AudioSource source;
}
