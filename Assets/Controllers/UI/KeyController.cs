using Assets.Managers;

public class KeyController : Observer
{
    public override void OnNotify(NotificationType notificationType, object value = null)
    {
        if(notificationType == NotificationType.PlayerPressedEsc)
        {
            GameManager.Instance.PauseGame();
            GameManager.Instance.GameWonDeactive();
            SceneGameManager.Instance.LoadMainMenu();
        }

        if(notificationType == NotificationType.CutsceneSkipped)
        {
            SceneGameManager.Instance.SetScene(SceneConstants.Demo);
        }
    }
}
