using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("Garden Environment");
    } 

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }


    public void Exit()
    {
        Application.Quit();
    }
}
