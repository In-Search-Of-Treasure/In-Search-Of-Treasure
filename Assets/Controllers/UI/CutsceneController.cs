using Assets.Managers;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CutsceneController : Observer
{
    public override void OnNotify(NotificationType notificationType, object value = null)
    {
        if (notificationType == NotificationType.CutsceneStopped)
        {
            Debug.Log("Player started the game and out cutscene.");
            GameManager.Instance.ResumeGame();
            SceneGameManager.Instance.SetScene(SceneManager.GetActiveScene().buildIndex + 1);
            GameManager.Instance.AlreadySawCutscene();            
        }
    }
}
