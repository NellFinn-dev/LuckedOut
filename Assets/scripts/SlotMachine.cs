using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : Entity
{
    #region instance variables

    public int startingHealth;
    public Animator uiAnim;

    #endregion

    #region methods

    // Start is called before the first frame update
    private void Start()
    {
        startingHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // If it's been hit
        if(startingHealth > health)
        {
            anim.SetTrigger("Spin");
            canBeHit = false;
        }
    }

    // Calls the luckManager to make a roll (Called during animation)
    public void callLuck()
    {
        GameObject.FindObjectOfType<luckManager>().luckRoll();
        uiAnim.SetTrigger("LuckEffect");
    }

    #endregion 
}
