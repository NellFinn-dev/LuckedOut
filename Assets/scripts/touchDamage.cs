using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchDamage : MonoBehaviour
{
    // Reference Variables
    #region instance variables 

    public enum entityType { Enemy, Player };
    public entityType type;
    public Rigidbody2D rb;
    public PlayerMovement playerMovementScript;

    #endregion

    #region default functions
    // Getting this reference
    private void Awake()
    {
        playerMovementScript = GameObject.FindFirstObjectByType<PlayerMovement>();
    }
    // Upon touching this object. damage is applied to the entity that touched it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && type == entityType.Enemy && !playerMovementScript.dashing)
        {
            Entity otherEntity = collision.GetComponent<Entity>();

            otherEntity.takeDamage(rb);
        }
        else
        {
            return;
        }

        if (collision.tag == "Enemy" && type == entityType.Player)
        {
            Entity otherEntity = collision.GetComponent<Entity>();

            otherEntity.takeDamage(rb);
        }
        else
        {
            return;
        }
    }

    #endregion
}
