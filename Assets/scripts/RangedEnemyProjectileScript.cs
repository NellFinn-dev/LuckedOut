using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyProjectileScript : MonoBehaviour
{
    #region instance variables
    
    public AudioManager AM;
    public RangedEnemy rangedEnemyScript;
    public Animator anim;
    public Transform[] ProjectileSpots;
    public GameObject Projectile;
    #endregion

    #region Methods
    public void Start()
    {
        AM = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void Shoot()
    {
            AM.Play("Shot");

            // For cam shake
            CameraShakeScript shakeScript = GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>();

            shakeScript.triggerShake();
            //rb.velocity = Vector2.MoveTowards(transform.position, Player.position, AttackSpeed * Time.deltaTime);

            rangedEnemyScript.Attacking = true;
            anim.SetTrigger("Attack");

            for (int i = 0; i < ProjectileSpots.Length; i++)
            {
                Instantiate(Projectile, ProjectileSpots[i].position, ProjectileSpots[i].rotation);
            }
    }

    #endregion
}
