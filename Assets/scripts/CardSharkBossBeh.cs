using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSharkBossBeh : Entity
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
    public float Closenes;
    public GameObject Projectile;
    public SpriteRenderer Animations;
    public float AttackSpeed;
    public Transform Back;
    public bool active;

    public float afterSwimRecoverTime;
    public float regularRecoverTime;

    public Animator[] FallingObjAnims;

    public Transform[] projectileSpots;
    // 0 is left and 1 is right

    public GameObject projectileHolder;

    public bool activeAnimFinished;

    public UnityEngine.AI.NavMeshAgent agent;

    public float activeAnimTime = 0.5f;

    public bool fastChase;

    public GameObject rouletteTable;
    public Transform[] rouletteSpawns;

    public bool fallingDiceCoolingDown;
    public float diceCooldownTime;

    public bool roulleteCoolingDown;
    public float roulleteCooldownTime;

    public GameObject spinningCardOb;
    public Transform[] cardPositions; // Position 0 is for the cards normally and position 1 is for if Cardshark can be hit
    public float cardSmoothing;

    public Animator gradeScreenTransition;

    #endregion

    #region default functions

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        TM = GameObject.FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        camShakeScript = GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();

    }

    private void FixedUpdate()
    {
        if (canBeHit)
        {
            spinningCardOb.transform.position = Vector2.Lerp(transform.position, cardPositions[1].position, cardSmoothing * Time.deltaTime);
        }
        else
        {
            spinningCardOb.transform.position = Vector2.Lerp(transform.position, cardPositions[0].position, cardSmoothing * Time.deltaTime);
        }


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

        if (health <= 0)
        {
            anim.SetTrigger("Down");

            // Scene transition trigger 
            gradeScreenTransition.SetTrigger("endScreenAnim");

            agent.speed = 0;
            return;
        }

        var delta = Player.position - transform.position;
        if (active)//!Attacking && active)
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

            Transform playerPos = GameObject.FindObjectOfType<Player>().transform;
            Vector3 direction = playerPos.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectileHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        
        if (Closenes > AttackRange && Attacking == false && !stunned)
        {
            agent.SetDestination(Player.position);
            transform.position = Vector2.MoveTowards(transform.position, Back.position, speed * Time.deltaTime);
            speed = RunSpeed;
            agent.speed = speed;

            if (agent.velocity.x != 0 || agent.velocity.y != 0)
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                if (!Attacking)
                {
                    anim.SetBool("Walking", false);
                }
            }
        }

        if (stunned)
        {
            agent.speed = 0;
        }

        if (Closenes <= AttackRange && Attacking == false)
        {
            int rand = Random.Range(0,5); // set to like 0, 3
            if(rand == 0)
            {
                StartCoroutine(DashAttack());
                Attacking = true;
            } else
            {
                // High key, get rid of this function call later
                AttackShuffle();
                // Instead of calling the cards method here, I will play the card shuffle animation and then use the animation trigger event to do it
                Attacking = true;
            }
         
            //.script.Flash();
            StartCoroutine(damageFlash(flashTime));
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

        if (Attacking && !fastChase)
        {
            agent.speed = 0;
            agent.SetDestination(transform.position);
        }

        if (fastChase)
        {
            agent.SetDestination(Player.position);
            transform.position = Vector2.MoveTowards(transform.position, Back.position, speed * Time.deltaTime);
            speed = RunSpeed * 2;
            agent.speed = speed;
        }


    }

    #endregion

    #region functions
    /*
    public void Dash()
    {
        if (health > 0)
        {
            //AM.Play("Shot");

            StartCoroutine(DashAttack());
            StartCoroutine("AttackReset");
            anim.SetTrigger("Dash");
            Attacking = true;
        }
    }
    */

    IEnumerator fallingDiceReset(float diceResetTime)
    {
        fallingDiceCoolingDown = true;
        yield return new WaitForSeconds(diceResetTime);
        fallingDiceCoolingDown = false;

    }

    IEnumerator RouletteReset(float roulleteResetTIme)
    {
        roulleteCoolingDown = true;
        yield return new WaitForSeconds(roulleteResetTIme);
        roulleteCoolingDown = false;

    }

    IEnumerator AttackReset(float AttackTime)
    {
        yield return new WaitForSeconds(AttackTime);

        Attacking = false;
        agent.speed = speed;
        canBeHit = false;
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

    IEnumerator DashAttack()
    {
        Debug.Log("Dash Attack");
        fastChase = true;
        Attacking = true;
        anim.SetBool("Swimming", true);
        // Fin in ground animation
        camShakeScript.triggerShake();
        yield return new WaitForSeconds(Random.Range(5,8));
        StartCoroutine(damageFlash(flashTime));
        agent.SetDestination(transform.position);
        speed = 0;
        agent.speed = speed;
        //Rumble
        yield return new WaitForSeconds(Random.Range(1, 2));
        fastChase = false;
        camShakeScript.triggerShake();
        //Pop up 
        // back to idle
        Attacking = true;
        canBeHit = true;
        anim.SetBool("Swimming", false);
        StartCoroutine(AttackReset(afterSwimRecoverTime));
    }

    public void AttackShuffle()
    {


        int attackRand = Random.Range(0, 4);

        switch (attackRand)
        {
            case 0:
                if (!roulleteCoolingDown)
                {
                    // Once animations are in these will be called b
                    spawnTables();
                    StartCoroutine(RouletteReset(roulleteCooldownTime));
                } else
                {
                    AttackShuffle();
                }
                break;
            case 1:
                if (!fallingDiceCoolingDown)
                {
                    // Once animations are in these will be called by the animator
                    spawnFallingDice();
                    StartCoroutine(fallingDiceReset(diceCooldownTime));
                }
                else
                {
                    AttackShuffle();
                }
                break;
            default:
                // Once animations are in these will be called by the animator
                throwCards();
                break;
            
        }
        
        /*
        
        Debug.Log("Card Attack");
        if (health > 0)
        {
            canBeHit = true;
            Attacking = true;
            agent.SetDestination(gameObject.transform.position);
            agent.speed = 0;
 
            int rand = Random.Range(0, 5);

            // Falling Slot machines
            if(rand == 0)
            {
                spawnFallingDice();
            }
            // Spinning Roullete tables
            if (rand == 1)
            {
                spawnTables();
                // Use the actual animation for the attacks StartCoroutine("AttackReset");
                // Spawn Roulette tables
            }
            // Throw Cards
            if (rand > 1)
            {
                throwCards();
            }
        }

        */
    }

    public void spawnFallingDice()
    {
        canBeHit = true;
        Attacking = true;

        Debug.Log("Falling Slot machines");
        for (int i = 0; i < FallingObjAnims.Length; i++)
        {
            int fallRandom = Random.Range(0, 2);
            if (fallRandom > 0)
            {
                FallingObjAnims[i].SetTrigger("Fall");
            }
        }
        StartCoroutine(AttackReset(regularRecoverTime));
    }

    public void throwCards()
    {
        canBeHit = true;
        Attacking = true;

        Debug.Log("Card Throw");
        // Play the throwing animation here
        for (int i = 0; i < projectileSpots.Length; i++)
        {
            Instantiate(Projectile, projectileSpots[i].position, projectileSpots[i].rotation);
        }
        StartCoroutine(AttackReset(regularRecoverTime));
    }

    public void spawnTables()
    {
        canBeHit = true;
        Attacking = true;

        Debug.Log("Spinning Tables");
        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                Instantiate(rouletteTable, rouletteSpawns[0].position, rouletteSpawns[0].rotation);
                break;
            case 1:
                Instantiate(rouletteTable, rouletteSpawns[1].position, rouletteSpawns[2].rotation);
                break;
            case 2:
                Instantiate(rouletteTable, rouletteSpawns[2].position, rouletteSpawns[2].rotation);
                break;
            case 3:
                Instantiate(rouletteTable, rouletteSpawns[0].position, rouletteSpawns[0].rotation);
                Instantiate(rouletteTable, rouletteSpawns[2].position, rouletteSpawns[2].rotation);
                break;
        }

        StartCoroutine(AttackReset(regularRecoverTime));

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
