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

    private bool alreadySawCutscene => GameManager.playerAlredySawCutscene;

    private void Awake()
    {
        var isGamePaused = GameManager.isGamePaused;
        var isGameOver = GameManager.isGameOver;
        var isGameWon = GameManager.isGameWon;        

        if (isGamePaused)
        {
            StartBtn.gameObject.SetActive(false);
            ResumeBtn.gameObject.SetActive(true);
        }

        if (isGameOver)
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            AboutMenu.SetActive(false);
            LoadingScreen.SetActive(false);
            WinnerMenu.SetActive(false);
            GameOverMenu.SetActive(true);

            StartBtn.gameObject.SetActive(true);
            ResumeBtn.gameObject.SetActive(false);
        }

        if (isGameWon)
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(false);
            AboutMenu.SetActive(false);
            LoadingScreen.SetActive(false);
            WinnerMenu.SetActive(true);
            GameOverMenu.SetActive(false);

            StartBtn.gameObject.SetActive(true);
            ResumeBtn.gameObject.SetActive(false);
        }
    }

    public void PlayGame()
    {        
        if (alreadySawCutscene)
        {
            Debug.Log("Player started the game.");
            GameManager.Instance.ResumeGame();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 2);
        }
        else
        {
            Debug.Log("Player started cutscene.");
            GameManager.Instance.ResumeGame();
            SceneGameManager.Instance.SetScene(SceneManager.GetActiveScene().buildIndex + 1);
            GameManager.Instance.AlreadySawCutscene();
        }
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
