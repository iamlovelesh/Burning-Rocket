using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    public void retry()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
