using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] listEnemies;

    private int[] sign = new int[] { -1, 1 };

    private void Start()
    {
        Vector3 randomOffset = RandomSpawn();
        Instantiate(listEnemies[0], player.transform.position + randomOffset, Quaternion.identity);
    }

    private Vector3 RandomSpawn()
    {
        int signX = sign[Random.Range(0, 1)];
        int signY = sign[Random.Range(0, 1)];
        Vector3 randomPos = new Vector3(signX* Random.Range(2.0f, 4.0f), signY * Random.Range(2.0f, 4.0f), 0);
        return randomPos;
    }
}
