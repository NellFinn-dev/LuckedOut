using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rb;
    public float Timer;
    public float TimeAmmount;
    public bool destroys;
    public void Start()
    {
        Timer = TimeAmmount;
    }

    public void FixedUpdate()
    {

        if (destroys)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        rb.velocity = transform.up * Speed;

        //transform.position += transform.up * Speed;
    }
}
