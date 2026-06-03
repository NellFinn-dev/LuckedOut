using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulleteAttack : MonoBehaviour 
{ 
    #region variables
    [Header("Spawn Settings")]
    public float spawnXMin = -8f;
    public float spawnXMax = 8f;

    [Header("Drop Settings")]
    public Vector2 dropAreaMin;
    public Vector2 dropAreaMax;
    public float dropSpeed = 8f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float speedIncreasePerBounce = 1.2f;
    public float activeTime = 3f;

    [Header("Effects")]
    public GameObject wallHitEffect;
    public LayerMask wallLayer; // Assign your wall layer here

    private Vector2 moveDir;
    private bool hasLanded = false;
    private float timer;
    private Vector2 landingPoint;
    private Rigidbody2D rb;
    public CameraShakeScript shakeScript;
    #endregion

    #region Unity Functions
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        /*
        float spawnX = Random.Range(spawnXMin, spawnXMax);
        transform.position = new Vector2(spawnX, transform.position.y);
        */

        landingPoint = new Vector2(
            Random.Range(dropAreaMin.x, dropAreaMax.x),
            Random.Range(dropAreaMin.y, dropAreaMax.y)
        );

        timer = activeTime;

        shakeScript = GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();
    }

    void Update()
    {
        if (!hasLanded)
        {
            rb.simulated = false;
            HandleDrop();
        }
        else
        {
            rb.simulated = true;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (hasLanded)
        {
            rb.velocity = moveDir * moveSpeed;
        }
    }

       void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0 && hasLanded)
        {
            shakeScript.triggerShake();
            // Reflect direction based on collision normal
            Vector2 normal = collision.contacts[0].normal;
            moveDir = Vector2.Reflect(moveDir, normal).normalized;
            // Speed up
            moveSpeed *= speedIncreasePerBounce;
            // Spawn effect
            if (wallHitEffect != null)
            {
                Instantiate(wallHitEffect, collision.contacts[0].point, Quaternion.identity);
            }
        }
    }
    #endregion

    #region methods
    void HandleDrop()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            landingPoint,
            dropSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, landingPoint) < 0.1f)
        {
            hasLanded = true;
            PickNewDirection();
        }
    }

    void PickNewDirection()
    {
        moveDir = Random.insideUnitCircle.normalized;
        shakeScript.triggerShake();
    }
    #endregion
}

