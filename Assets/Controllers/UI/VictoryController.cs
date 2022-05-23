using Assets.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : Subject
{
    private const string VictoryScene = "Fase-05";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.General.Player))
        {
            var currentScene = SceneManager.GetActiveScene();

            if(currentScene.name == VictoryScene)
            {
                GameManager.Instance.PauseGame();
                SceneGameManager.Instance.LoadGameWon();
            }
            else
            {
                SceneManager.LoadScene(currentScene.buildIndex + 1);
            }
                        
        }
    }
}
