using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GradeScript : MonoBehaviour
{
    #region Instance Variables

    [SerializeField] 
    private FloatSO scores;

    public float totalPoints;

    public TextMeshProUGUI text;
    public Color sRankColor;

    #endregion

    // End Screen grading text
    public void Grade()
    {
        float timePoints  = scores.Time;
        float comboPoints = scores.BestCombo;

        //FindObjectOfType<PlayerInputs>().enabled = false;

        totalPoints = comboPoints;

        // Penalise slower times (higher time = worse for a speedrun)
        if (timePoints >= 50) 
        {
            totalPoints -= 35;
        }
        else if (timePoints >= 40) 
        {
            totalPoints -= 25;
        }
        else if (timePoints > 30) 
        {
            totalPoints -= 15;
        }

        // Assign grade
        string grade;

        if (totalPoints >= 40) 
        {
            grade = "S";
            text.color = sRankColor;
        }
        else if (totalPoints >= 35)
        {
            grade = "A+";
        }
        else if (totalPoints >= 30)  
        {
            grade = "A";
        }
        else if (totalPoints >= 25)
        {  
            grade = "B";
        }
        else       
        {                 
            grade = "C";
        }
        
        text.text = grade;
    }
}
