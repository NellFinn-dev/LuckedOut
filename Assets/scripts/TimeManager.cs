using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // This script is a time manager for the whole game but will for now just be used for hit stop time manipulation

    public float hitstopTime;
    public void Start()
    {
        Time.timeScale = 1f;
    }

    public void slowTime()
    {
        Time.timeScale = .3f;
    }

    // Trigger method because just running the IEnumerator wasn't working
    public void triggerHitStop()
    {
        StartCoroutine(hitStop());
    }

    // Actual time stop
    public IEnumerator hitStop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitstopTime);
        Time.timeScale = 1f;
    }
}
