using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] listEnemiesPrefabs;
    public float[] listEnemiesSpeed {
        get { return m_listEnemiesSpeed; }
    }

    private float[] m_listEnemiesSpeed = new float[] { 2 };
    private int[] sign = new int[] { -1, 1 };

    private IEnumerator spawnerCoroutine;

    private void Start()
    {
        // Call coroutine to spawn enemies waves
        // TODO: wait all enemies died and spawnNextWave? true to call respawn
        spawnerCoroutine = WaitAndSpawn(2.0f);
        StartCoroutine(spawnerCoroutine);
    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            // Create a random location for the spawn
            Vector3 randomSpawnLoc = RandomLocation();

            // Choose a random index for the enemy to spawn
            int randomEnemyIndex = Random.Range(0, listEnemiesPrefabs.Length);

            // Instantiate the enemy
            Instantiate(listEnemiesPrefabs[randomEnemyIndex], player.transform.position + randomSpawnLoc, Quaternion.identity);
        }
    }

    private Vector3 RandomLocation()
    {
        // Random sign for [X] & [Y] axis
        int signX = sign[Random.Range(0, 1)];
        int signY = sign[Random.Range(0, 1)];

        // Generate a random location between 4 clusters
        Vector3 randomLoc = new Vector3(signX * Random.Range(2.0f, 4.0f), signY * Random.Range(2.0f, 4.0f), 0);

        return randomLoc;
    }

}
