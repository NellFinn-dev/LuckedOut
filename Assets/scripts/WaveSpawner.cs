using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    #region instance variables

    public GameObject[] enemyPrefabs;
    // Boss Enemies tend to be already in the level because they have more variables to instantiate and set 
    public GameObject[] bossPrefabs;

    public int[] waveAmounts;

    [SerializeField] private int enemyCount;
    [SerializeField] private int waveNumber = 1;

    public float timeBetweenEnemySpawn;
    public float timeBetweenWaves;

    public Transform[] spawnPoints;

    bool spawningWave;

    public GameObject[] doors;
    public GameObject leaveArrow;
    public bool started;
    public bool ended;

    #endregion

    #region methods
    // Start is called before the first frame update
    public void startSpawner()
    {
        //Starting spawner
        StartCoroutine(SpawnEnemyWave(waveAmounts[waveNumber]));
        // Activate the door
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(true);
        }

        // Soudns and cam shake
        //GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("closedDoors");
        //GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>().triggerShake();

        started = true;
    }

    public void endSpawner()
    {
        // Deactivate doors
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }

        // Soudns and cam shake
        //GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>().Play("closedDoors");
        //GameObject.FindObjectOfType<CameraShakeScript>().GetComponent<CameraShakeScript>().triggerShake();
        leaveArrow.SetActive(true);
        ended = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawningWave)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        if (!started)
        {
            return;
        }

        if (enemyCount == 0 && !spawningWave)
        {
            if (waveAmounts.Length > waveNumber)
            {
                waveNumber++;
                spawningWave = true;
                StartCoroutine(SpawnEnemyWave(waveAmounts[waveNumber - 1]));
                return;
            } else
            {
                if (!ended && !spawningWave)
                {
                    endSpawner();
                }
            }
        }
    }

    IEnumerator SpawnEnemyWave(int enemiesToSpawn)
    {
        Debug.Log($"Wave starting. Waiting {timeBetweenWaves} seconds...");
        spawningWave = true;

        // 1. Initial delay
        yield return new WaitForSeconds(timeBetweenWaves);

        // 2. Spawn Regular Enemies
        // Check if the array exists AND has at least one prefab inside it
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                // Pick random prefab and random spawn point
                GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                Debug.Log($"Spawned enemy {i + 1} of {enemiesToSpawn}");

                yield return new WaitForSeconds(timeBetweenEnemySpawn);
            }
        }
        else
        {
            Debug.LogWarning("enemyPrefabs array is empty or null!");
        }

        // 3. Activate Bosses
        // Use bossPrefabs.Length to avoid "Index Out of Range" errors 
        if (bossPrefabs != null && bossPrefabs.Length > 0)
        {
            Debug.Log("Activating bosses...");
            for (int i = 0; i < bossPrefabs.Length; i++)
            {
                if (bossPrefabs[i] != null)
                {
                    bossPrefabs[i].SetActive(true);
                    yield return new WaitForSeconds(timeBetweenEnemySpawn);
                }
            }
        }

        spawningWave = false;
        Debug.Log("Wave spawning complete.");

        spawningWave = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Starting the spawner when the player walks in but only if it hasn't started yet
        if(collision.CompareTag("Player") && !started)
        {
            startSpawner();
        }
    }
    #endregion
}
