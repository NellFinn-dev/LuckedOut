using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectSpawner : MonoBehaviour
{
    #region variables
    public Entity entityScript;
    public GameObject[] hitEffectsRand;
    public Transform[] hitEffectSpawns;

    public GameObject[] hitEffectsSpecific;
    #endregion

    #region methods
    public void SpawnRand()
    {
        // Rotating the hit spots
        if (entityScript.facingRight)
        {
            for (int i = 0; i < hitEffectSpawns.Length; i++)
            {
                hitEffectSpawns[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            for (int i = 0; i < hitEffectSpawns.Length; i++)
            {
                hitEffectSpawns[i].transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }



        for (int i = 0; i < hitEffectSpawns.Length; i++)
        {
            for (int j = 0; j < hitEffectsRand.Length; j++)
            {
                Instantiate(hitEffectsRand[j], hitEffectSpawns[i].position, hitEffectSpawns[i].rotation);
            }
        }
    }

    public void spawnOneEffect(int effectIndex, int spotIndex)
    {
        // Rotating the hit spots
        if (entityScript.facingRight)
        {
            for (int i = 0; i < hitEffectSpawns.Length; i++)
            {
                hitEffectSpawns[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            for (int i = 0; i < hitEffectSpawns.Length; i++)
            {
                hitEffectSpawns[i].transform.rotation = Quaternion.Euler(0, -180, 0);
            }
        }

        Instantiate(hitEffectsSpecific[effectIndex], hitEffectSpawns[spotIndex].position, hitEffectSpawns[spotIndex].rotation);
        
    }
    #endregion
}
