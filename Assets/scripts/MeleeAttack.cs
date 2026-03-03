using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    #region instance variables 

    public float attackRange;
    public LayerMask targetLayer;

    public Rigidbody2D rb;

    public Transform attackSpot;

    public Transform[] leftAndRightSpots;

    [SerializeField] private Entity lastHit;
    public Player playerScript;

    // For sounds
    public AudioManager AM;
    public TimeManager TM;
    public ControllerRumble rumbleScript;

    #endregion

    #region default methods 

    private void Start()
    {
        // Getting reference for the AudioManager
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        playerScript = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        TM = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        rumbleScript = GameObject.FindObjectOfType<ControllerRumble>().GetComponent<ControllerRumble>();
    }

    #endregion


    #region do damage
    // method to check for an enetity to damage in a range and calls that entity's damage methiod
    public void attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackSpot.position, attackRange, targetLayer);

        for (int i = 0; i < hit.Length; i++)
        {
            hit[i].GetComponent<Entity>().takeDamage(rb);
            lastHit = hit[i].GetComponent<Entity>();
        }

        // For playing a random option out of 3 different attack sounds
        #region play a sound from a selection
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                AM.Play("Punch1");
                break;
            case 1:
                AM.Play("Punch2");
                break;
            case 2:
                AM.Play("Punch3");
                break;
        }

    }


    // Changes the attack spot based on the facing direction of this enetity
    private void FixedUpdate()
    {

        if(playerScript.facingRight)
        {
            attackSpot.position = leftAndRightSpots[0].position;
        } else
        {
            attackSpot.position = leftAndRightSpots[1].position;
        }
    }

    #endregion

    #endregion

    #region draw 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackSpot.position, attackRange);
    }


    #endregion
}
