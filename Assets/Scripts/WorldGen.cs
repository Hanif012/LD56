using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [SerializeField] private GameObject[] worldPrefabs;
    [SerializeField] private float worldLength = 1000f;
    [SerializeField] private float spawnDelay = 1f;

    private void Start()
    {
        StartCoroutine(SpawnWorld());
    }

    private IEnumerator SpawnWorld()
    {
        for (int i = 0; i < worldLength; i++)
        {
            int randomIndex = Random.Range(0, worldPrefabs.Length);
            Vector3 randomPosition = new Vector3(Random.Range(-worldLength, worldLength), 0, Random.Range(-worldLength, worldLength));
            Instantiate(worldPrefabs[randomIndex], randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
