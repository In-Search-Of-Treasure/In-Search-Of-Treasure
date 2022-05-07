using Assets.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public Button StartBtn;
    public Button ResumeBtn;

    private void Awake()
    {
        var isGamePaused = GameManager.isGamePaused;

        if (isGamePaused)
        {
            StartBtn.gameObject.SetActive(false);
            ResumeBtn.gameObject.SetActive(true);
        }
    }

    public void PlayGame()
    {
        Debug.Log("Player started the game.");        
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

    public void ExitGame()
    {
        Debug.Log("Player quit the game.");
        Application.Quit();
    }
}
