using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to be called during animations to play sounds
public class AnimationPlaySound : MonoBehaviour
{
    #region instance variables

    // For sounds
    public AudioManager AM;

    #endregion

    #region default functions

    private void Awake()
    {
        // Getting reference for the AudioManager
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void playSound(string name)
    {
        AM.Play(name);
    }

    public void endSound(string name)
    {
        AM.Stop(name);
    }

    #endregion
}
