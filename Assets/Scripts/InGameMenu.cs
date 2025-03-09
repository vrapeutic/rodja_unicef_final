using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
   public void BackToMainMenu()
   {
        SceneManager.LoadScene(0);
   }

    public void ExitGame()
    {
        Application.Quit();
    }
}
