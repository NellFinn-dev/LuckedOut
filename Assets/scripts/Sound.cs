using UnityEngine;

[System.Serializable]
public class Sound 
{
    #region variables
    public string name;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0f, 1f)]
    public float pitch = 1;
    public bool loop = false;
    [HideInInspector]
    public AudioSource source;
    #endregion

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.Play();
    }
   
}
