using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{

    // Make sure to add the disabling of controls during an action to this script;

    #region instance variables

    public PlayerInputActions playerControls;
    public Vector2 moveDirection;
    public Vector2 lastmoveDirection;

    public InputAction move;
    public InputAction punch;
    public InputAction dash;
    public InputAction restart;

    public PlayerMovement movementScript;
    public PlayerAttack attackScript;

    [SerializeField] private PlayerAnimations animScript;

    [SerializeField] private Player playercontrol;

    public bool levelEnded;

    public bool restarting;
    public Animator restartAnim;
    // For sounds
    public AudioManager AM;

    public Entity entityScript;

    #endregion

    #region Input setting

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        // Getting reference for the AudioManager
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        OnEnable();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        punch = playerControls.Player.Punch;
        punch.Enable();
        punch.performed += Punch;

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;

        restart = playerControls.Player.Restart;
        restart.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        punch.Disable();
        dash.Disable();
    }

    #endregion

    #region Movement related stuff
  
    public void Update()
    {

        restarting = playerControls.Player.Restart.IsPressed();

        if (playercontrol.canPerformActions && !levelEnded)
        {
            moveDirection = move.ReadValue<Vector2>().normalized;

            if (moveDirection.x != 0 || moveDirection.y != 0)
            {
                lastmoveDirection = moveDirection;
            }

            if (restarting)
            {
                restartAnim.SetBool("Restarting" ,true);
            }else
            {
                restartAnim.SetBool("Restarting", false);
            }
        }

        if(lastmoveDirection.x > 0)
        {
            entityScript.facingRight = true;
        } else if (lastmoveDirection.x < 0)
        {
            entityScript.facingRight = false;
        }

        if(levelEnded == true && GameObject.FindObjectOfType<Player>().GetComponent<Entity>().health > 0)
        {
            moveDirection.x = 1;
            moveDirection.y = 0;
        }
    }

    public void Punch(InputAction.CallbackContext context)
    {
        // if (playercontrol.canPerformActions || animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchL") && !levelEnded) add this back for one punch
        if (playercontrol.canPerformActions || animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchL") && !levelEnded)
            attackScript.onPunch();

        if (playercontrol.canPerformActions || animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchR") && !levelEnded)
            attackScript.onPunch();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (playercontrol.canPerformActions && !levelEnded)
        {
            movementScript.StartCoroutine("DashEvent", movementScript.dashTime);
        }

        // Play Dash sound
        AM.Play("Dash");
    }

    #endregion

}
