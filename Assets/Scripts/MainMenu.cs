using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Scene NextScene;
    public void PlayGame()
    {
        SceneManager.LoadScene(NextScene.buildIndex);
    }
}
