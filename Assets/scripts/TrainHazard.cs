using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHazard : MonoBehaviour
{
    public float trainTimer, trainTime;
    public bool active;
    public Transform[] trainSpots;
    public GameObject train;
    public float warningTime;

    public void FixedUpdate()
    {
        if (active)
        {
            if (trainTimer == 0)
            {
                StartCoroutine("trainSpawn", warningTime);
            } else
            {
                trainTimer -= Time.deltaTime;
            }
        }    
    }

        public IEnumerator trainSpawn (float warningTime)
        {
            int rand = Random.Range(0, 2);
            // Cardshark animation
            // Warning Triangles 
            // Low level cam shake

            // Trains will be treated as projectiles 
            yield return new WaitForSeconds(warningTime);
            if (rand == 0)
            {
                Instantiate(train, trainSpots[0].position, trainSpots[0].rotation);
            }
            else
            {
                Instantiate(train, trainSpots[1].position, trainSpots[1].rotation);
            }

            trainTimer = trainTime;
        }
}
