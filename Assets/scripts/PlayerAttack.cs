using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    #region instance variables

    public float punchTime;
    public float afterPunchStun;
    [SerializeField] private PlayerInputs inputScript;
    [SerializeField] private PlayerMovement movementScript;

    public float punchSpeed;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Player playercontrol;
    [SerializeField] private PlayerAnimations animScript;

    private Vector2 moveDir;
    private Vector2 lastmoveDir;

    public bool attacking;
    public bool inPunch;

    // For sounds
    [SerializeField] private AudioManager AM;
    
    #endregion

    #region setting variables

    private void Start()
    {
        // gets the refrence for the rigidbody2D variable
        rb = playercontrol.rb;
        // Getting reference for the AudioManager
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void FixedUpdate()
    {
        // Grabs input and saves the last input directions
        moveDir = inputScript.moveDirection;
        lastmoveDir = inputScript.lastmoveDirection;

        moveDir = inputScript.move.ReadValue<Vector2>();
        // Calls the animations for attacking and uses that to determine the attacking variable (bool)
        attacking = animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchL") || animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchR");
        
    }

    #endregion

    #region punching 
    // if the punching is happening then the tirggers for punching will be set
    public void onPunch()
    {
        // Checking if animations are playing to do combo stuff

        if (animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchL"))
        {
            animScript.anim.SetTrigger("PunchR"); 
        } 
        
        if (animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchR"))
        {
            animScript.anim.SetTrigger("PunchL");
        }

        if (!animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchL") && !animScript.anim.GetCurrentAnimatorStateInfo(0).IsName("PunchR"))
        { 
            animScript.anim.SetTrigger("PunchL");
        }

        //else (for just two punches delete the next if)
    }

    // IEnumerator for punch sliding
    public IEnumerator PunchSlide()
    {
        rb.velocity = new Vector3(lastmoveDir.x, lastmoveDir.y, 0f) * punchSpeed;

        yield return new WaitForSeconds(punchTime);

        rb.velocity = Vector3.zero;

        yield return null;
    }


    #endregion
}
