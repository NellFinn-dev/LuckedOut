using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    #region instance variables
    // Instance Variables
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] Player playerScript;
    public Animator anim;
    public PlayerAttack attackScript;
    public SpriteRenderer playerRenderer;

    public CameraShakeScript camShake;

    [SerializeField] private PlayerInputs inputScript;

    public Transition transitionScript;
    
    public GameObject[] leftParticles;
    public GameObject[] rightParticles;

    public Entity playerEntityScript;
    public Color[] playerColors; // 0 For normal 1 for ghost/transparent

    #endregion

    #region animations
    // Setting the camShake variable
    private void Start()
    {
        camShake = GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();
        playerEntityScript = GameObject.FindObjectOfType<Player>().GetComponent<Entity>();
    }

    private void Update()
    {
        
        if(playerScript.entityScript.health <= 0)
        {
            anim.SetTrigger("Downed");
        }

        // Movment input animation control
        if(inputScript.moveDirection.x != 0 || inputScript.moveDirection.y != 0)
        {
            anim.SetBool("Moving", true);
        } else
        {
            anim.SetBool("Moving", false);
        }
        // Last inputted movement & Player after image effect and sprite flipping
        if(inputScript.lastmoveDirection.x < 0)
        {
            playerRenderer.flipX = true;
            playerScript.facingRight = false;

            for (int i = 0; i < leftParticles.Length; i++)
            {
                leftParticles[i].SetActive(true);
                rightParticles[i].SetActive(false);
            }

        } else
        {
            playerRenderer.flipX = false;
            playerScript.facingRight = true;

            for (int i = 0; i < rightParticles.Length; i++)
            {
                rightParticles[i].SetActive(true);
                leftParticles[i].SetActive(false);
            }
        }
        // Dashing animation
        if (movementScript.dashing)
        {
            anim.SetBool("Dashing", true);
        }else
        {
            anim.SetBool("Dashing", false);
        }

        // Color changing and ghost time
        if (playerEntityScript.ghostTime)
        {
            playerRenderer.color = playerColors[1];
        } else
        {
            playerRenderer.color = playerColors[0];
        }
    }

    // The slide forward for when the player punches
    public void PunchSlide()
    {
        attackScript.StartCoroutine("PunchSlide");
    }
    // Triggers cam shake
    public void triggerCamShake()
    {
        camShake.triggerShake();
    }
    // Triggers thentransition between scenes
    public void triggerTransition()
    {
        transitionScript.restart = true;
        transitionScript.transitionTrigger();
    }

    public void callDirectionCheck()
    {
        inputScript.directionCheck();

        if(!playerEntityScript.facingRight)
        {
            playerRenderer.flipX = true;
            playerScript.facingRight = false;

            for (int i = 0; i < leftParticles.Length; i++)
            {
                leftParticles[i].SetActive(true);
                rightParticles[i].SetActive(false);
            }

        } else
        {
            playerRenderer.flipX = false;
            playerScript.facingRight = true;

            for (int i = 0; i < rightParticles.Length; i++)
            {
                rightParticles[i].SetActive(true);
                leftParticles[i].SetActive(false);
            }
        }
    }   
    #endregion


}
