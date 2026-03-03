using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLine : MonoBehaviour
{
    public SpeedRunTimer timerScript;
    public ComboManager comboScript;
    public GameObject winScreen;
    public GradeScript gradingScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            endLevel();
        }
    }

    public void endLevel()
    {
        comboScript.endLevel();
        timerScript.Check();
        gradingScript.Grade();

        winScreen.SetActive(true);
    }
}
