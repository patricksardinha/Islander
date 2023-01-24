using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject player;
    private EnemiesController enemiesController;

    private float randomCurrentSpeed;

    private void Start()
    {
        // Find Gameobjects & components
        player = GameObject.Find("Player");
        enemiesController = GameObject.Find("EnemiesController").GetComponent<EnemiesController>();

        // Define a random speed for each enemy spawned 
        randomCurrentSpeed = enemiesController.listEnemiesSpeed[Random.Range(0, enemiesController.listEnemiesSpeed.Length)];
    }

    private void Update()
    {
        // Chase the player to hit him 
        Chase();
    }
    private void Chase()
    {
        // Update position from [EnemyPos] -> towards [PlayerPos] with enemy's speed
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, randomCurrentSpeed * Time.deltaTime);
    }
}
