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
    public GameObject cardSpin;

    #endregion

    #region methods
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

    // Chooses between a good luck and bad luck roll
    public void luckRoll()
    {
        // Seeing if it'd be more fun and better for the story to only have buffs from the Luck machine
            goodLuckRoll();
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
            StartCoroutine(cardsOff());
            GameObject.FindObjectOfType<Player>().GetComponent<Entity>().ghostTimeCall(invincibilityTime); 
        }

        if (rand == 3)
        {
            // Find a third effect
        }
    }

    IEnumerator cardsOff()
    {
        cardSpin.SetActive(true);
        yield return new WaitForSeconds(invincibilityTime);
        cardSpin.SetActive(false);
    }
    #endregion
} 
