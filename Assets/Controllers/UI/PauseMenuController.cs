using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : Observer
{
    public GameObject pauseMenu;

    private bool isPaused;

    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        if (isPaused)
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = 0;
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

    public override void OnNotify(NotificationType notificationType, object value = null)
    {
        if (notificationType == NotificationType.PlayerPressedEsc)
        {
            Pause();
        }
    }
}
