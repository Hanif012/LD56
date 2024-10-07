using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public UIGameplayManager uiGameplayManager;
    [SerializeField] public GameObject playerPrefab;
    [SerializeField] public EnemySpawner[] enemySpawner;
    [SerializeField] public int Level = 0;
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
    [SerializeField] public Text LevelText;

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
            uiGameplayManager.Pause();
        }
        StartCoroutine(AutoSkipLevel());
    }

    private bool isAutoSkipping = false;

    public IEnumerator AutoSkipLevel()
    {
        if (gameState == GameState.Playing && !isAutoSkipping)
        {
            isAutoSkipping = true;
            yield return new WaitForSeconds(30f);
            StartCoroutine(SkipLevel());
            isAutoSkipping = false;
        }
    }
    public IEnumerator SkipLevel()
    {
        isLevelingUp = true;
        
        if (enemySpawner.Length > 0)
        {
            // Spawn a random enemy from the spawner array
            for(int i = 0; i < enemySpawner.Length; i++)
            {
                Debug.Log("Spawning enemy from spawner " + i);
                StartCoroutine(enemySpawner[i].SpawnEnemy());
            }
        }
        Level++;
        LevelText.text = "Level: " + Level;
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