using Assets.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button StartBtn;
    public Button ResumeBtn;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject AboutMenu;
    public GameObject LoadingScreen;
    public GameObject GameOverMenu;
    public GameObject WinnerMenu;    

    private void Start()
    {
        var isGamePaused = GameManager.isGamePaused;
        var isGameOver = GameManager.isGameOver;
        var isGameWon = GameManager.isGameWon;

        if (isGameOver && isGamePaused)
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            AboutMenu.SetActive(false);
            LoadingScreen.SetActive(false);
            WinnerMenu.SetActive(false);
            GameOverMenu.SetActive(true);

            StartBtn.gameObject.SetActive(true);
            ResumeBtn.gameObject.SetActive(false);
            GameManager.Instance.GameOverDeactive();
        }
        else if (isGameWon && isGamePaused)
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            AboutMenu.SetActive(false);
            LoadingScreen.SetActive(false);
            WinnerMenu.SetActive(true);
            GameOverMenu.SetActive(false);

            StartBtn.gameObject.SetActive(true);
            ResumeBtn.gameObject.SetActive(false);
            GameManager.Instance.GameWonDeactive();
        }
        else if (isGamePaused)
        {
            StartBtn.gameObject.SetActive(false);
            ResumeBtn.gameObject.SetActive(true);
        }
    }

    public void PlayGame()
    {                
        Debug.Log("Player started cutscene.");
        GameManager.Instance.ResumeGame();
        SceneGameManager.Instance.SetScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResumeGame()
    {
        Debug.Log("Player resumes the game.");
        GameManager.Instance.ResumeGame();
        var sceneToResume = PlayerPrefs.GetInt(PlayerPrefConstants.SceneRelated.SavedScene);

        if (sceneToResume != 0)
            SceneManager.LoadScene(sceneToResume);
        else
            return;
    }

    public void GameOver()
    {
        Debug.Log("Player looses. Game Over!");
        GameManager.Instance.GameOverDeactive();
    }

    public void WonGame()
    {
        Debug.Log("Player won the game. Congrats!");
        GameManager.Instance.GameWonActive();
    }

    public void ExitGame()
    {
        Debug.Log("Player quit the game.");
        Application.Quit();
    }

    private void ScreenTransition(MenuScreenTransition transition)
    {
        var gameOverTransition = transition == MenuScreenTransition.GameOver;
        var gameWonTransition = transition == MenuScreenTransition.GameWon;
        var gamePausedTransition = transition == MenuScreenTransition.PausedGame;

        MainMenu.SetActive(gamePausedTransition);
        OptionsMenu.SetActive(false);
        AboutMenu.SetActive(false);
        LoadingScreen.SetActive(false);
        WinnerMenu.SetActive(gameWonTransition);
        GameOverMenu.SetActive(gameOverTransition);

        StartBtn.gameObject.SetActive(!gamePausedTransition);
        ResumeBtn.gameObject.SetActive(gamePausedTransition);

        switch (transition)
        {
            case MenuScreenTransition.GameWon:
                GameManager.Instance.GameWonDeactive();
                break;            
        }
    }
}
