using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Instance variable
    public Sound[] sounds;


    #region methods
    // On Awake
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Does what it says it does
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.Play();
 
    }
    // Does what it says it does
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    // Does what it says it does
    public void ChangePitch(string name, float newPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            // Update the data variable so FixedUpdate picks it up
            s.pitch = newPitch;
        }
        //s.source.pitch = pitch;
    }
    #endregion
}
