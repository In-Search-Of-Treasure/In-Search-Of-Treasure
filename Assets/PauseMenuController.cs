using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{

    public CanvasGroup pauseCanvasGroup;
    bool isPaused = false;

    void Start()
    {
        pauseCanvasGroup.alpha = 0;
    }

    // Update is called once per frame
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
}
