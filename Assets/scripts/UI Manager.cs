using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Used for the darkening of the screen
    [Header("Roll mechanics")]
    public Animator darkenScreenAnim;
    public bool rollActivated;

    public GameObject rollScreen;
    public bool canRoll;

    public float rollTimeCooldownBeforeStart;

    [Header("Time control")]
    public float timeChangeRate;

    public void DarkenScreenToggle(bool darken)
    {
        // Used to toggle the screen darkening effect
        if(darken)
        {
            darkenScreenAnim.SetBool("Darkened", true);
        } else
        {
            darkenScreenAnim.SetBool("Darkened", false);
        }
    }

    // Sets up the screen for rolling
    public void ActivateRoll()
    {
        StartCoroutine("rollReset", rollTimeCooldownBeforeStart);

        rollActivated = true;
        DarkenScreenToggle(true);
        rollScreen.SetActive(true);
        // Consider deactivating player controls and the navMesh
    }

    // Turns off all the rolling stuff
    public void DeactivateRoll()
    {
        rollActivated = false;
        DarkenScreenToggle(false);
        //Time.timeScale = 1;
        rollScreen.SetActive(false);
        canRoll = false;
        GameObject.FindObjectOfType<PlayerInputs>().GetComponent<PlayerInputs>().enabled = true; // Enables the player input script
    }

    public IEnumerator rollReset(float resetTime)
    {
        canRoll = false;
        yield return new WaitForSeconds(resetTime);
        canRoll = true;
        GameObject.FindObjectOfType<PlayerInputs>().GetComponent<PlayerInputs>().enabled = false; // Disables the player input script
        //Time.timeScale = 0;
    }

}
