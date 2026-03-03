using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    #region instance variables
    public Transition endScreen;

    public bool autoMoveEnding;

    #endregion

    #region methods
    // Sets the end screen transition object to active when the 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            endScreen.restart = false;
            endScreen.transitionTrigger();

            // Makes the player automatically keep moving right only if this ending is specifically an automove one

            if (autoMoveEnding && GameObject.FindObjectOfType<Player>().GetComponent<Entity>().health > 0)
            {
                GameObject.FindObjectOfType<PlayerInputs>().GetComponent<PlayerInputs>().levelEnded = true;
            }
        }
    }

    #endregion
}
