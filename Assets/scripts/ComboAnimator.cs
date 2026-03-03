using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboAnimator : MonoBehaviour
{
    #region instance variable

    public Animator anim;
    public TextMeshProUGUI comboWordText;
    public TextMeshProUGUI comboText;
    public ComboManager comboManage;

    // Colors to change the combo text to based off of combo number
    public Color[] colors;
    #endregion

    #region methods

    // Getting the cariable of comboManage
    private void Start()
    {
        comboManage = GameObject.FindObjectOfType<ComboManager>().GetComponent<ComboManager>();
    }

    // Method that shakes the text while it's out
    public void shakeText()
    {
        if (comboManage.combo < 2)
        {
            // Color 1
            comboText.color = colors[0];
            comboWordText.color = colors[0];
        } else if (comboManage.combo < 3)
        {
            // Color 2
            comboText.color = colors[1];
            comboWordText.color = colors[1];
        }
        else if (comboManage.combo < 4)
        {
            // Color 3
            comboText.color = colors[2];
            comboWordText.color = colors[2];
        }
        else
        { 
            // Color 4
            comboText.color = colors[3];
            comboWordText.color = colors[3];
        }

        anim.SetTrigger("Shake");
    }

    #endregion
}
