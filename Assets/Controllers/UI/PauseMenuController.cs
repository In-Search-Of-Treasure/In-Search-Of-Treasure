using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{

    public CanvasGroup pauseCanvasGroup;
    bool isPaused = false;
    private const string MENU = "Menu";

    void Start()
    {
        pauseCanvasGroup.alpha = 0;
        isPaused = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        if (!isPaused)
        {
            pauseCanvasGroup.alpha = 1;
            isPaused = !isPaused;
            Time.timeScale = 0;
        }
        else
        {
            pauseCanvasGroup.alpha = 0;
            isPaused = !isPaused;
            Time.timeScale = 1;
        }
    }

    public void ReloadCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame()
    {
        Debug.Log("Player quit the game.");
        SceneManager.LoadScene(SceneConstants.Menu);
    }
}
