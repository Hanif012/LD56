using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public EnemySpawner[] enemySpawner;
    [SerializeField] public int Level = 1;
    [SerializeField] public float levelDelay = 1f;
    [SerializeField] public enum GameState
    {
        Playing,
        Pause,
        LevelUp,
        GameOver
    };
    
    [SerializeField] public GameState gameState = GameState.Pause;
    private bool isLevelingUp = false;


    private void Start()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isLevelingUp && gameState == GameState.Playing)
        {
            StartCoroutine(SkipLevel());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState = GameState.Pause;
        }
    }
    public IEnumerator SkipLevel()
    {
        isLevelingUp = true;
        
        if (enemySpawner.Length > 0)
        {
            // Spawn a random enemy from the spawner array
        
        }
        Level++;
        yield return new WaitForSeconds(levelDelay);
        
        // Call SpawnEnemy() method from the EnemySpawner script
        for(int i = 0; i < enemySpawner.Length; i++)
        {
            Debug.Log("Spawning enemy from spawner " + i);
            StartCoroutine(enemySpawner[i].SpawnEnemy());
        }

        isLevelingUp = false;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

}