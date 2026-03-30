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
    }

    #endregion

    #region movement

    private void Update()
    {

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
        dashing = true;

        attackScript.attackCancel();

        for (int i = 0; i < dashEffects.Length; i++)
        {
            dashEffects[i].SetActive(true);
        }

        rb.velocity = new Vector3(lastmoveDir.x, lastmoveDir.y, 0f) * dashSpeed;
       
        yield return new WaitForSeconds(dashTime);
        dashing = false;
        for (int i = 0; i < dashEffects.Length; i++)
        {
            dashEffects[i].SetActive(false);
        }

    }

    #endregion
}
