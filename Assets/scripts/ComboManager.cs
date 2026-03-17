using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    #region instance variables
    public int combo;
    public int bestCombo;
    public float comboTimer;
    public float timerReset;

    public FloatSO scoreScript;
    public ComboAnimator Comboanim;
    public AudioManager AM;

    public float currentPitch;
    #endregion

    #region methods

    // Getting the variable of Comboanim
    private void Start()
    {
        Comboanim = GameObject.FindObjectOfType<ComboAnimator>().GetComponent<ComboAnimator>();
        bestCombo = (int) scoreScript.BestCombo;
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }


    private void FixedUpdate()
    {
        // Runs the timer down and resets it and the combo score when it hits 0
        if(comboTimer <= 0)
        {
            combo = 0;
            comboTimer = timerReset;
            currentPitch = .5f;
        } else
        {
            comboTimer -= Time.deltaTime;
        }

        scoreScript.BestCombo = bestCombo;
        scoreScript.Combo = combo;
    }

    //Outdated name from when enemies where defeated in one hit
    public void enemyDowned()
    {
        // Adds 1 to the combo counter on downing and enemy and resets the timer
        combo++;

        if(combo > bestCombo)
        {
            bestCombo = combo;
        }

        comboTimer = timerReset;


        Comboanim.shakeText();
        Comboanim.comboText.text = "x" + combo + "!";

        //For playing the increasing in pitch sound
        if (currentPitch < 1f)
        {
            currentPitch += .05f;
            AM.ChangePitch("HitSound", currentPitch);
        }

        AM.Play("HitSound");

        // Add color changing code here
    }

    // Called when the level ends to update the floatSO object
    public void endLevel()
    {
        scoreScript.BestCombo = bestCombo;
    }

    #endregion
}
