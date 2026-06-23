using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region instance varibles

    public Rigidbody2D rb;
    public Entity entityScript;
    public PlayerMovement movementScript;
    public PlayerAttack attackScript;
    public bool canPerformActions;
    public bool facingRight;
    public float fullHealth;

    public Animator luckyFaceAnim;
    public bool iFrames;

    #endregion

    #region setting variables

    private void Start()
    {
        rb = entityScript.rb;

        fullHealth = entityScript.health;
    }

    #endregion

    #region Update

    private void FixedUpdate()
    {
        #region stun state

        if (movementScript.dashing || attackScript.attacking || entityScript.stunned)
        {
            canPerformActions = false;
        } else
        {
            canPerformActions = true;
        }

        #endregion

        #region health image

        if (entityScript.health >= fullHealth)
        {
            //Not Damaged
            //if(!luckyFaceAnim.GetCurrentAnimatorStateInfo(0).IsName(""))
            luckyFaceAnim.SetTrigger("Good");
        }

        if (entityScript.health == fullHealth - 1 && entityScript.health != 0)
        {
            //Damaged a qaurter
            luckyFaceAnim.SetTrigger("Mid");
        }

        if (entityScript.health == fullHealth - 2 && entityScript.health != 0)
        {
            //Damaged half
            luckyFaceAnim.SetTrigger("Bad");
        }

        if (entityScript.health <= 0)
        {
            //Damaged most
            luckyFaceAnim.SetTrigger("Down");
        }

        #endregion
    }

    public void gameOver()
    {

    }

    public void gainHealth()
    {
        entityScript.health++;
    }

    public void loseHealthOnRoll()
    {
        if (entityScript.health > 1)
        {
            entityScript.health--;
        }
    }

    #endregion
}
