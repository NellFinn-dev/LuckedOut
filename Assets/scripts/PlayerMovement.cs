using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    #region instance variables

    public float dashTime;

    public float moveSpeed;
    [SerializeField] private float dashSpeed;
    private Rigidbody2D rb;
    [SerializeField] private Player playercontrol;
    public Entity entityScript;

    private Vector2 moveDir;
    private Vector2 lastmoveDir;

    public PlayerInputs inputScript;

    public bool dashing;
    public GameObject[] dashEffects;

    public PlayerAttack attackScript;


    // For sounds
    public AudioManager AM;

    #endregion

    #region setting variables
    // Find the component for the variable rb
    private void Start()
    {
        rb = playercontrol.rb;
        // Getting reference for the AudioManager
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        attackScript = GameObject.FindObjectOfType<PlayerAttack>().GetComponent<PlayerAttack>();
        entityScript = GameObject.FindObjectOfType<Player>().GetComponent<Entity>();
    }

    #endregion

    #region movement


    public Vector2 momentumforTesting;


    private void Update()
    {

        // 
        momentumforTesting = rb.velocity;

        //

        // Setting move dir and lastmovedir
        moveDir = inputScript.moveDirection;
        lastmoveDir = inputScript.lastmoveDirection;

        //moveDir = inputScript.move.ReadValue<Vector2>();

        // Calls the move method
        if (playercontrol.canPerformActions && GameObject.FindObjectOfType<Player>().GetComponent<Entity>().health > 0 && !dashing)
        {
            Move();
        }
        
    }

    // Moves the player's rigidbody2D component with the movedir and movespeed variables
    public void Move()
    {
        rb.velocity = new Vector3(moveDir.x, moveDir.y, 0f) * moveSpeed;
    }


    // Event that handles dashing
    public IEnumerator DashEvent (float dashTime)
    {
        //Resetting velocity to combat momentum from punching
        inputScript.directionCheck();
        attackScript.attackCancel();
        
        //rb.velocity = Vector3.zero; the attack script does this, I'm just keeping it here so I don't forget it happens

        dashing = true;
        entityScript.canBeHit = false;

        for (int i = 0; i < dashEffects.Length; i++)
        {
            dashEffects[i].SetActive(true);
        }

        rb.velocity = new Vector3(lastmoveDir.x, lastmoveDir.y, 0f) * dashSpeed;
       
        yield return new WaitForSeconds(dashTime);
        
        for (int i = 0; i < dashEffects.Length; i++)
        {
            dashEffects[i].SetActive(false);
        }

        dashing = false;

        yield return new WaitForSeconds(0.2f);
        
        entityScript.canBeHit = true;

    }

    #endregion
}
