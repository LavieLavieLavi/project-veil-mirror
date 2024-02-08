using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerNetwork : NetworkBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>(); // List of enemy prefabs
    public List<Transform> spawnPoints = new List<Transform>(); // List of spawn points
    public float spawnInterval = 2f; // Interval between spawns

    void Start()
    {
        if (isServer)
        {
            // Start spawning enemies repeatedly
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        // Ensure there are spawn points and enemy prefabs available
        if (spawnPoints.Count == 0 || enemyPrefabs.Count == 0)
        {
            Debug.LogError("No spawn points or enemy prefabs assigned to the EnemySpawnerNetwork script.");
            yield break;
        }

        while (true)
        {
            // Choose a random enemy prefab from the list
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // Choose a random spawn point from the list
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Spawn the enemy at the chosen spawn point
            GameObject enemy = Instantiate(randomEnemyPrefab, randomSpawnPoint.position, Quaternion.identity);

            // Spawn the enemy on the network
            NetworkServer.Spawn(enemy);

            // Wait for the specified interval before spawning again
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
