using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Entity
{
    #region instance variables

    [Header("Instance variables")]
    public float speed;
    public float RunSpeed;
    public Transform Player;
    public float AttackRange;
    public float AvoidRange;
    public float ActiveRange;
    public bool Attacking;
    public float AttackTime;
    public float Closenes;
    public Transform Attackspot;
    public bool facingRight;
    public SpriteRenderer Animations;
    public float AttackSpeed;
    public Transform Back;
    public bool active;

    public bool activeAnimFinished;

    public NavMeshAgent agent;

    public float activeAnimTime = 0.5f;

    public GameObject[] hitBoxes;
    // R is 1 and L is 2


    #endregion

    #region default functions

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        // Getting reference for the AudioManager
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        camShakeScript = GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();
    }

    private void FixedUpdate()
    {
        if(health <= 0)
        {
            anim.SetTrigger("Down");
            agent.speed = 0;
            return;
        }

        var delta = Player.position - transform.position;
        if (!Attacking && active)
        {
            if (delta.x >= 0)
            { // mouse is on right side of player
                Animations.flipX = false;
                facingRight = true;
            }
            else if (delta.x < 0)
            { // mouse is on left side
                Animations.flipX = true;
                facingRight = false;
            }
        }

        //Checking distance and weather to Attack or to move
        Closenes = Vector2.Distance(transform.position, Player.position);

        if (!active)
        {
            if (Closenes < ActiveRange)
            {
                anim.SetTrigger("Active");
                StartCoroutine(activeAnimation(activeAnimTime));
                // Wait until animation finishes and then start movement
            }

            return;
        }

        
        if (Closenes > AttackRange && Attacking == false && !stunned)
        {
            agent.SetDestination(Player.position); 
            transform.position = Vector2.MoveTowards(transform.position, Back.position, speed * Time.deltaTime);
            speed = RunSpeed;
            agent.speed = speed;

            /*
            if(agent.velocity.x != 0 || agent.velocity.y != 0)
            {
                anim.SetBool("Running", true);
            } else
            {
                anim.SetBool("Running", false);
            }
            */
        }


        // Deciding wether to set the running animation trigger to true or false
        if (agent.velocity.x != 0 || agent.velocity.y != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }


        if (Closenes <= AttackRange && Attacking == false)
        {
            Attack();
            //.script.Flash();
        }

        //Moving towards the player
        if (Closenes < AvoidRange && Closenes > AttackRange && Attacking == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Back.position, speed * Time.deltaTime);
        }

        if (Closenes < AvoidRange && Closenes < AttackRange && Attacking == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }

        if(Attacking || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") 
            || anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            agent.speed = 0;
            agent.SetDestination(transform.position);
        }

        //For activating the left and right hitboxes

        if (facingRight)
        {
            hitBoxes[0].SetActive(true);
            hitBoxes[1].SetActive(false);
        } else
        {
            hitBoxes[1].SetActive(true);
            hitBoxes[0].SetActive(false);
        }

    }

    #endregion


    #region functions

    public void Attack()
    {
        if (health > 0)
        {
            Attacking = true;
            StartCoroutine("AttackReset");
            anim.SetTrigger("Attack");
            agent.speed = 0;
            //rb.velocity = Vector2.MoveTowards(transform.position, Player.position, AttackSpeed * Time.deltaTime);
        }
    }

    IEnumerator AttackReset()
    {
        agent.speed = 0;
        yield return new WaitForSeconds(AttackTime);

        Attacking = false;
        agent.speed = speed;
    }

    IEnumerator activeAnimation(float animTime)
    {
        var delta = Player.position - transform.position;
        if (!Attacking && active)
        {
            if (delta.x >= 0)
            { // mouse is on right side of player
                Animations.flipX = false;
                facingRight = true;
            }
            else if (delta.x < 0)
            { // mouse is on left side
                Animations.flipX = true;
                facingRight = false;
            }
        }

        yield return new WaitForSeconds(animTime);
        active = true;
        stunned = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackspot.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Attackspot.position, AvoidRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Attackspot.position, ActiveRange);
    }

    #endregion
}
