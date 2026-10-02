using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : Entity
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public Sprite[] healthSprites;

    // 3 hp, not damage, 2 hp, damaaged, 1 hp, damaged 2, 0hp, destroyed 
    void Update()
    {
        switch(health)
        {
            case 0:
                spriteRenderer.sprite = healthSprites[3];
                break;
            case 1:
                spriteRenderer.sprite = healthSprites[2];
                break;
            case 2:
                spriteRenderer.sprite = healthSprites[1];
                break;
            case 3:
                spriteRenderer.sprite = healthSprites[0];
            break;
            default:
                // Handle other states
                break;
        }
    }
}
