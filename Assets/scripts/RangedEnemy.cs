using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Entity
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
    public Transform[] ProjectileSpots;
    public GameObject Projectile;
    public SpriteRenderer Animations;
    public float AttackSpeed;
    public Transform Back;
    public bool active;

    public Transform[] projectileSpots;
    // 0 is left and 1 is right

    public GameObject projectileHolder;

    public bool activeAnimFinished;

    public UnityEngine.AI.NavMeshAgent agent;

    public float activeAnimTime = 0.5f;

 
    #endregion

    #region Unity functions

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        TM = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        camShakeScript = GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();
    }

    private void FixedUpdate()
    {
        if (health <= 0)
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
                    projectileHolder.transform.position = projectileSpots[1].position;
                    projectileHolder.transform.rotation = projectileSpots[1].rotation;
                }
                else if (delta.x < 0)
                { // mouse is on left side
                    Animations.flipX = true;
                    facingRight = false;
                    projectileHolder.transform.position = projectileSpots[0].position;
                    projectileHolder.transform.rotation = projectileSpots[0].rotation;
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


            if (Closenes > AttackRange && Attacking == false)
            {
                agent.SetDestination(Player.position);
                transform.position = Vector2.MoveTowards(transform.position, Back.position, speed * Time.deltaTime);
                speed = RunSpeed;
                agent.speed = speed;

                if (agent.velocity.x != 0 || agent.velocity.y != 0)
                {
                    anim.SetBool("Running", true);
                }
                else
                {
                    anim.SetBool("Running", false);
                }
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

            if (Attacking)
            {
                agent.speed = 0;
                agent.SetDestination(transform.position);
            }


        }
    

    #endregion


    #region functions

    public void Attack()
    {
        if (health > 0)
        {

            AM.Play("Shot");

            // For cam shake
            CameraShakeScript shakeScript = GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();

            shakeScript.triggerShake();
            //rb.velocity = Vector2.MoveTowards(transform.position, Player.position, AttackSpeed * Time.deltaTime);


            Attacking = true;
            StartCoroutine("AttackReset");
            anim.SetTrigger("Attack");

            for (int i = 0; i < ProjectileSpots.Length; i++)
            {
                Instantiate(Projectile, ProjectileSpots[i].position, ProjectileSpots[i].rotation);
            }
            
        }
    }

    IEnumerator AttackReset()
    {
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
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AvoidRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ActiveRange);
    }

    #endregion

}