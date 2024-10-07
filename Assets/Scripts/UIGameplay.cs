using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject uiPause;
    [SerializeField] private GameObject uiGameOver;
    [SerializeField] private GameObject uiGameplay;

    public void Pause()
    {
        uiPause.SetActive(true);
        uiGameplay.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        uiPause.SetActive(false);
        uiGameplay.SetActive(true);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        uiGameOver.SetActive(true);
        uiGameplay.SetActive(false);
        Time.timeScale = 0;
    }

    void Start()
    {
        uiPause.SetActive(false);
        uiGameOver.SetActive(false);
        uiGameplay.SetActive(true);
    }
}
