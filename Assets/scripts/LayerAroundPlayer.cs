using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerAroundPlayer : MonoBehaviour
{
    #region instance variables
    private Transform Player;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public bool useAttatchedRenderer = true;
    #endregion

    #region methods

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        if(useAttatchedRenderer)
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // For rendering in front of or behind the player based on Y value
    void Update()
    {
        if(Player.position.y <= gameObject.transform.position.y)
        {
            spriteRenderer.sortingOrder = 0;
        } else
        {
            spriteRenderer.sortingOrder = 3;
        }
    }

    #endregion
}
