using Assets.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //TODO: GAMEOVER
    public Button StartBtn;
    public Button ResumeBtn;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject AboutMenu;
    public GameObject LoadingScreen;
    public GameObject GameOverMenu;

    private void Awake()
    {
        var isGamePaused = GameManager.isGamePaused;
        var isGameOver = GameManager.isGameOver;

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
            GameOverMenu.SetActive(true);
            StartBtn.gameObject.SetActive(true);
            ResumeBtn.gameObject.SetActive(false);
        }
    }

    public void PlayGame()
    {
        Debug.Log("Player started the game.");
        GameManager.Instance.ResumeGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
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

    public void ExitGame()
    {
        Debug.Log("Player quit the game.");
        Application.Quit();
    }
}
