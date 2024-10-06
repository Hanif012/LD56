using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float Range = 5;
    [SerializeField] private int spawnCount = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }
    
    private IEnumerator SpawnEnemiesCoroutine()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(1);
            Vector3 spawnPosition = new Vector3(Random.Range(0, Range), 0, Random.Range(0, Range));
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Enemy Spawned");
        }
    }
}
