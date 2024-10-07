using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Fathi";
    public void PlayGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
