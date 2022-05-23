using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : Observer
{
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
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

        if (pauseMenu.active)
        {
            pauseMenu.SetActive(!pauseMenu.active);
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(!pauseMenu.active);
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
