using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRange = 1f; // Renamed to follow naming conventions
    [SerializeField] private int spawnCounter = 5;
    [SerializeField] private GameObject[] EnemyPrefab;

    void Start()
    {
        // check if there is null variable
        if (EnemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is null");
        }
    }
    
    public IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < spawnCounter; i++)
        {
            yield return new WaitForSeconds(1f); // Spawns one enemy every second
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            spawnCounter++;
            // Instantiate a random enemy from the EnemyPrefab array
            Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], spawnPosition, Quaternion.identity);
            Debug.Log("Enemy Spawned");
        }
    }
}