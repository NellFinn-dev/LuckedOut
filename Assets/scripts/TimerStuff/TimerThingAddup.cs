using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerThingAddup : MonoBehaviour
{
    #region instance variables
    public TextMeshProUGUI Timetxt;
    public TextMeshProUGUI BestCombo;
    public TextMeshProUGUI BestTime;
    public float ammount;
    [SerializeField]
    private FloatSO timeSO;

    public GradeScript gradeScript;
    public GameObject newBestCombo;
    public GameObject newBestTime;

    #endregion

    #region methods
    // Displays text for the scoring
    private void OnEnable()
    {
        if(timeSO.Time < timeSO.BestTime || timeSO.BestTime == 0)
        {
            timeSO.BestTime = timeSO.Time;
            newBestTime.SetActive(true);
        }

        if(timeSO.Combo > timeSO.BestCombo)
        {
            timeSO.BestCombo = timeSO.Combo;
            newBestCombo.SetActive(true);
        }

        Timetxt.text =  timeSO.Time.ToString("f2") + "s";
        BestCombo.text = "" + timeSO.BestCombo;
        BestTime.text = "" + timeSO.BestTime.ToString("f2") + "s";

        gradeScript.Grade();
    }

    #endregion

}
