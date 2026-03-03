using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class luckManager : MonoBehaviour
{
    #region instance variables

    public int enemiesDowned;
    public int totalEnemiesDowned;
    public int luckScore;
    public TextMeshProUGUI luckText;
    public Animator diceAnim;

    public bool canDownEnemy;
    public float downCooldown;

    public UIManager uiScript;
    // Bounds used for testing if the selection point is in the green box
    public Transform leftBound;
    public Transform rightBound;
    public GameObject selectionBox;

    public bool diceAnimTriggered;

    public Image greenBar;
    public float [] greenbarSizex;

    public Animator sliderAnim;

    public roomManager roomManagerscript;
    public SlotMachineSpawner[] slotSpawners;
    public bool slotmachineSpawned;

    // Time for ghost time if rolled 
    public float invincibilityTime;
    public float onePunchTime;
    #endregion

    private void Start()
    {
        roomManagerscript = GameObject.FindObjectOfType<roomManager>().GetComponent<roomManager>();
    }

    // For when an enemy is downed
    // Gets called and adds to the total enemies and when it reaches 7, it resets and calls the luckRoll mehtod
    public void downedEnemy()
    {
        enemiesDowned++;
        totalEnemiesDowned++;

        if (enemiesDowned >= 2 && !diceAnimTriggered)
        {
            diceAnim.SetTrigger("Roll");
            diceAnimTriggered = true;
        }
    }

    public void slotMachineSpawn()
    {
        if (diceAnimTriggered && !slotmachineSpawned)
        {
            slotmachineSpawned = true;
            slotSpawners[roomManagerscript.roomSelected].SpawnMachine();
        }
    }

    /*
    // Makes the roll of the dice 

    // Called directly through the PlayerInput component
    public void luckRoll()
    {
        // Popup for an undertale style timing mechanic to decide a roll if it's not already up

        if (enemiesDowned >= 7)
        {
            // Activate the mini game

            int rand = Random.Range(1, 7);

            if (rand == 1)
            {
                luckText.text = "You rolled an extra hitpoint!";
                GameObject.FindObjectOfType<Player>().gainHealth();
            }

            if (rand == 2)
            {
                luckText.text = "You rolled temp invinicibility!";
            }

            if (rand == 3)
            {
                luckText.text = "You rolled 10s off the clock!";
            }

            if (rand == 4)
            {
                luckText.text = "You rolled only 1HP left!";
            }

            if (rand == 5)
            {
                luckText.text = "You rolled slower speed!";
            }

            if (rand == 6)
            {
                luckText.text = "You took 1 damage!";
            }

             enemiesDowned = 0;
             diceAnimTriggered = false;
            return;
        }

    }
    */

    // Called directly through the PlayerInput component
    /* Older code
    public void luckRoll()
    {
        // If the player's health is at 0 or lower than the function returns
        if(GameObject.FindObjectOfType<Player>().GetComponent<Entity>().health <= 0)
        {
            return;
        }

        // Popup for an undertale style timing mechanic to decide a roll if it's not already up

        if (enemiesDowned >= 7 && !uiScript.rollActivated)
        {
            // Activate the mini game

            uiScript.ActivateRoll();
            diceAnimTriggered = false;
            enemiesDowned = 0;

            // Changes the size of the bar
            greenBar.transform.localScale = new Vector3(Random.Range(greenbarSizex[0], greenbarSizex[1]), 0.43751f, 1);
            // Changes the slider speed
            sliderAnim.speed = Random.Range(0.5f, 2);
            // Bar will just have a collider and be a random size and the knob will move at random speed
            return;
        }

        if (uiScript.rollActivated && uiScript.canRoll)
        {
            if (selectionBox.transform.position.x >= leftBound.position.x &&
               selectionBox.transform.position.x <= rightBound.position.x)
            {
                // Roll for good luck

                uiScript.DeactivateRoll();
                goodLuckRoll();
                Debug.Log("Good Roll");
            }
            else
            {
                // Roll for bad luck

                uiScript.DeactivateRoll();
                badLuckRoll();
                Debug.Log("Bad Roll");
            }
        }
    }
    */

    // Chooses between a good luck and bad luck roll
    public void luckRoll()
    {
        //int rand = Random.Range(1, 3);

        //if(rand == 1)
        //{

        // Seeing if it'd be more fun and better for the story to only have buffs from the Luck machine
            goodLuckRoll();
        //} else
        //{
        //    badLuckRoll();
        //}
    }
    
    // Mostly just understandable
    public void goodLuckRoll()
    {
        // Back to 1, 4 after testing
        int rand = Random.Range(1, 4);

        if (rand == 1)
        {
            luckText.text = "You rolled an extra hitpoint!";
            GameObject.FindObjectOfType<Player>().gainHealth();
        }

        if (rand == 2)
        {
            luckText.text = "You rolled temp invinicibility!";
            // This method is called in the Entity script since it was already written and is used in other cases
            GameObject.FindObjectOfType<Player>().GetComponent<Entity>().ghostTimeCall(invincibilityTime); 
        }

        if (rand == 3)
        {
            // Find a third effect
        }
    }

    /*
    IEnumerator tempLongerDashes(float time)
    {
        PlayerMovement movementScript = GameObject.FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        float dashTimeTemp = movementScript.dashTime;
        movementScript.moveSpeed = dashTimeTemp * 1.5f;
        yield return new WaitForSeconds(time);
        movementScript.moveSpeed = dashTimeTemp;
    }
    */


}


#region old roll dice code
/*
    public int enemiesDowned;
    public int totalEnemiesDowned;
    public int luckScore;
    public TextMeshProUGUI luckText;
    public Animator diceAnim;

    public bool canDownEnemy;
    public float downCooldown;

    public UIManager uiScript;
    // Bounds used for testing if the selection point is in the green box
    public Transform leftBound;
    public Transform rightBound;
    public GameObject selectionBox;

    public bool diceAnimTriggered;

    public Image greenBar;
    public float [] greenbarSizex;

    public Animator sliderAnim;

    // For when an enemy is downed
    // Gets called and adds to the total enemies and when it reaches 7, it resets and calls the luckRoll mehtod
    public void downedEnemy()
    {
            enemiesDowned++;
            totalEnemiesDowned++;

        if (enemiesDowned >= 7 && !diceAnimTriggered)
        {
            diceAnim.SetTrigger("Roll");
            diceAnimTriggered = true;
        }
    }

    // Makes the roll of the dice 

    // Called directly through the PlayerInput component
    public void luckRoll()
    {
        // Popup for an undertale style timing mechanic to decide a roll if it's not already up

        if (enemiesDowned >= 7 && !uiScript.rollActivated)
        {
            // Activate the mini game

            uiScript.ActivateRoll();
            diceAnimTriggered = false;
            enemiesDowned = 0;

            // Changes the size of the bar
            greenBar.transform.localScale = new Vector3 (Random.Range(greenbarSizex[0], greenbarSizex[1]), 1, 1);
            // Changes the slider speed
            sliderAnim.speed = Random.Range(0.5f, 2);
            // Bar will just have a collider and be a random size and the knob will move at random speed
            return;
        }

        if (uiScript.rollActivated && uiScript.canRoll)
        {
           if(selectionBox.transform.position.x >= leftBound.position.x &&
              selectionBox.transform.position.x <= rightBound.position.x)
            {
                // Roll for good luck

                uiScript.DeactivateRoll();
                goodLuckRoll();
                Debug.Log("Good Roll");
            }
            else
            {
                // Roll for bad luck

                uiScript.DeactivateRoll();
                badLuckRoll();
                Debug.Log("Bad Roll");
            }
        }
    }

    public void goodLuckRoll()
    {
        int rand = Random.Range(1,4);

        if(rand == 1)
        {
            luckText.text = "You rolled an extra hitpoint!";
        }

        if (rand == 2)
        {
            luckText.text = "You rolled temp invinicibility!";
        }

        if (rand == 3)
        {
            luckText.text = "You rolled 10s off the clock!";
        }
    }

    public void badLuckRoll()
    {
        int rand = Random.Range(1, 4);

        if (rand == 1)
        {
            luckText.text = "You rolled only 1HP left!";
        }

        if (rand == 2)
        {
            luckText.text = "You rolled slower speed!";
        }

        if (rand == 3)
        {
            luckText.text = "You took 1 damage!";
        }
    }
 */
#endregion // Old commented code