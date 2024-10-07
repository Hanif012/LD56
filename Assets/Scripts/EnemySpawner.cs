using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRange = 1f; 
    [SerializeField] private int spawnCounter = 1;
    [SerializeField] private GameObject[] EnemyPrefab;

    void Start()
    {
        // check if there is null variable
        if (EnemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is null");
        }
    }
    void Update()
    {
        
    }
    
    public IEnumerator SpawnEnemy()
    {
        for (int i = 0; i <  spawnCounter; i++)
        {
            yield return new WaitForSeconds(1f); // Spawns one enemy every second
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnRange, spawnRange), 
                0, 
                Random.Range(-spawnRange, spawnRange)
            );
            // Instantiate a random enemy from the EnemyPrefab array
            Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], spawnPosition, Quaternion.identity);
            Debug.Log("Enemy Spawned");
        }
        spawnCounter = Mathf.CeilToInt(spawnCounter * 1.1f);
    }
}