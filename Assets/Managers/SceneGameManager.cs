using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGameManager : MonoBehaviour
{
    public static SceneGameManager Instance { get; private set; }

    private int currentSceneIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SetScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadMainMenu()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(PlayerPrefConstants.SceneRelated.SavedScene, currentSceneIndex);
        SceneManager.LoadScene(SceneConstants.Menu);
    }
}
