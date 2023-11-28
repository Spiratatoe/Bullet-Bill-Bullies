using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.doesLoop;
        }
    }
    
    private void Start() {
        
    }

    public void Play(string soundname) {
        Sound soundToPlay = null; 
        foreach (Sound s in sounds) {
            if (s.name == soundname) {
                soundToPlay = s;
                break;
            }
        } 
        if (soundToPlay != null) { 
            Debug.Log("Playing: " + soundname);
            soundToPlay.source.Play(); 
        }
    }

    public void Stop(string soundname) {
        Sound soundToPlay = null; 
        foreach (Sound s in sounds) {
            if (s.name == soundname) {
                soundToPlay = s;
                break;
            }
        } 
        if (soundToPlay != null) { soundToPlay.source.Stop(); }    
    }
}
