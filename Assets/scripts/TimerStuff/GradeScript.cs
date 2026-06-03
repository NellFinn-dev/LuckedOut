using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GradeScript : MonoBehaviour
{
    #region instance variables
    [SerializeField]
    private FloatSO _Scores;

    public float Points1;
    public float Points2;
    public float TotalPoints;

    public TextMeshProUGUI text;

    #endregion

    // End Screen grading text
    public void Grade()
    {
        Points1 = _Scores.Time;
        Points2 = _Scores.BestCombo;

        GameObject.FindObjectOfType<PlayerInputs>().enabled = false;

        TotalPoints += Points2;

        if (Points1 >= 50)
        {
            TotalPoints -= 4;
        }

        if (Points1 <= 40 && Points1 > 30)
        {
            TotalPoints -= 3;
        }

        if (Points1 <= 30 && Points1 > 20)
        {
            TotalPoints -= 2;
        }

        if (Points1 <= 20 && Points1 > 10)
        {
            TotalPoints -=  1;
        }


        if(TotalPoints <= 2)
        {
            text.text = "C";
        }

        if (TotalPoints >= 3 && TotalPoints < 5)
        {
            text.text = "B";
        }

        if (TotalPoints >= 5 && TotalPoints < 7)
        {
            text.text = "A";
        }
       
        if ( TotalPoints >= 7)
        {
            text.text = "A+";
        }

        if (TotalPoints >= 10)
        {
            text.text = "S";
        }

    }
}
