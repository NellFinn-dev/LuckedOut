using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GradeScript : MonoBehaviour
{
    #region instance variables
    [SerializeField]
    private FloatSO scores;

    public float points1;
    public float points2;
    public float totalPoints;

    public TextMeshProUGUI text;

    #endregion

    // End Screen grading text
    public void Grade()
    {
        points1 = scores.Time;
        points2 = scores.BestCombo;

        GameObject.FindObjectOfType<PlayerInputs>().enabled = false;

        totalPoints += points2;

        if (points1 >= 50)
        {
            totalPoints -= 4;
        }

        if (points1 <= 40 && points1 > 30)
        {
            totalPoints -= 3;
        }

        if (points1 <= 30 && points1 > 20)
        {
            totalPoints -= 2;
        }

        if (points1 <= 20 && points1 > 10)
        {
            totalPoints -=  1;
        }


        if(totalPoints <= 2)
        {
            text.text = "C";
        }

        if (totalPoints >= 3 && totalPoints < 5)
        {
            text.text = "B";
        }

        if (totalPoints >= 5 && totalPoints < 7)
        {
            text.text = "A";
        }
       
        if ( totalPoints >= 7)
        {
            text.text = "A+";
        }

        if (totalPoints >= 10)
        {
            text.text = "S";
        }

    }
}
