using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region instance variables
    [Header("General Enemy variables")]

    public Rigidbody2D rb;

    public SpriteRenderer renderer;

    public int health;

    public bool canBeHit;

    public float knockbackSpeed;
    public float knockbackTime;
    public float stunnedTime = 0.5f;

    public bool stunned;

    public Animator anim; 
    
    public enum entityType { Enemy, Player };
    public entityType type;

    public bool down;

    public CameraShakeScript camShakeScript;

    public float flashTime;

    public Material[] materials;

    public bool ghostTime;

    public effectSpawner effectScript;

    public bool facingRight;

    // For sounds
    public AudioManager AM;

    //Time Manager
    public TimeManager TM;

    //Instantiating variables 
    public bool variablesInsantiated;

    #endregion

    #region default Functions

    private void Awake()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag("luckManager");
        if (managerObj != null)
        {
            AM = managerObj.GetComponent<AudioManager>();
            TM = managerObj.GetComponent<TimeManager>();
        }
        else
        {
            Debug.LogError("Could not find object with tag 'luckManager'!");
        }

        camShakeScript = FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();

    }

    #endregion

    #region damage and knockback

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the damage happens to this entity script it calls the take damage method
        if(collision.CompareTag("DamagePlayer"))
        {
            if (type == entityType.Player)
            {
                takeDamage(collision.GetComponent<Rigidbody2D>());
                // True for high rumble
                //rumbleScript.callRumble(true);
            }
        }

        if (collision.CompareTag("DamageEnemy"))
        {
            if (type == entityType.Enemy)
            {
                takeDamage(collision.GetComponent<Rigidbody2D>());
                // False for low rumble 
                //rumbleScript.callRumble(false);
            }
        }
    }

    public void takeDamage(Rigidbody2D other)
    {
        // For playing a random option out of 3 different hit sounds
        #region play a sound from a selection
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                AM.Play("Hit");
                break;
            case 1:
                AM.Play("Hit");
                break;
            case 2:
                AM.Play("Hit");
                break;
        }

            TM.triggerHitStop();

        // If the entity can be damaged, the damage is taken and knockback is applied
        if (canBeHit && health > 0)
        {
            anim.SetTrigger("Hit");

            effectScript.SpawnRand();

            health--;
            // Triggers the camShake script
            camShakeScript.triggerShake();

            if (type == entityType.Enemy)
            {
                GameObject.FindGameObjectWithTag("luckManager").GetComponent<ComboManager>().enemyDowned();

                other = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }

            StartCoroutine(knockback(knockbackTime, other));
            StartCoroutine(stunTime(stunnedTime));
            StartCoroutine(damageFlash(flashTime));

            if (!down && health <= 0)
            {
                down = true;

                if (type == entityType.Enemy)
                {
                    GameObject.FindGameObjectWithTag("luckManager").GetComponent<luckManager>().downedEnemy();
                    GameObject.FindGameObjectWithTag("luckManager").GetComponent<ComboManager>().enemyDowned();
                    Debug.Log("Ouch I've been downed");
                }

                if (type == entityType.Player)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>().enabled = false;
                    anim.SetTrigger("Downed");
                }
            }

            if(other.transform.position.x < transform.position.x)
            {
                facingRight = false;
            } else
            {
                facingRight = true;
            }
        }

        #endregion
        //StartCoroutine(hitStop(hitStopTime));
    }

    // Knockback IEnumerator

    // Handles stopping the time for a second
    /*
    public IEnumerator hitStop(float stopTime)
    {
        //anim.speed = 0f;
        //GameObject.FindObjectOfType<PlayerAnimations>().GetComponent<Animator>().speed = 0f;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(stopTime);
        Time.timeScale = 1f;
        //anim.speed = 1f;

        Debug.Log("Time stopped and started");
        // Just in case for if this is an enemy script
        //GameObject.FindObjectOfType<PlayerAnimations>().GetComponent<Animator>().speed = 1f;
    }
    */

    public IEnumerator knockback(float hitTime, Rigidbody2D other)
    {
        Debug.Log(other);

        rb.velocity = (rb.position - other.position).normalized * knockbackSpeed;

        yield return new WaitForSecondsRealtime(hitTime);

        rb.velocity = Vector3.zero;
    }

    // Stun IEnumerator
    public IEnumerator stunTime(float hitTime)
    {
        stunned = true;
        canBeHit = false;
        anim.SetBool("Running", false);
        yield return new WaitForSecondsRealtime(hitTime);
        stunned = false;

        if(type == entityType.Player)
        {
            StartCoroutine("ghostTimeEvent", hitTime * 2);
        } else
        {
            canBeHit = true;
        }
    }

    public void ghostTimeCall(float timeAsGhost)
    {
        StartCoroutine("ghostTimeEvent", timeAsGhost);
    }

    IEnumerator ghostTimeEvent (float timeAsGhost)
    {
        canBeHit = false;
        ghostTime = true;
        yield return new WaitForSecondsRealtime(timeAsGhost);
        ghostTime = false;
        canBeHit = true;
    }

    // IEnumerator to render the flashing for damage
    public IEnumerator damageFlash(float flashTime)
    {
        renderer.material = materials[1];

        yield return new WaitForSecondsRealtime(flashTime);

        renderer.material = materials[0];
    }

    #endregion

}
