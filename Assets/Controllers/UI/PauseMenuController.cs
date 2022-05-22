using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button continueButton;
    public Button restartButton;
    public Button exitButton;

    public CanvasGroup pauseCanvasGroup;
    bool isPaused = false;
    private const string MENU = "Menu";

    void Start()
    {
        pauseCanvasGroup.alpha = 0;
        isPaused = false;
        continueButton.enabled = false;
        restartButton.enabled = false;
        exitButton.enabled = false;

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
            continueButton.enabled = true;
            restartButton.enabled = true;
            exitButton.enabled = true;
            pauseCanvasGroup.alpha = 1;
            isPaused = !isPaused;
            Time.timeScale = 0;
        }
        else
        {
            continueButton.enabled = false;
            restartButton.enabled = false;
            exitButton.enabled = false;

            Debug.Log(restartButton.enabled);


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
        Application.Quit();
    }
}
