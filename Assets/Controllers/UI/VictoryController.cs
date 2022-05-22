using Assets.Managers;
using UnityEngine;

public class VictoryController : Subject
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.General.Player))
        {
            GameManager.Instance.PauseGame();
            SceneGameManager.Instance.LoadGameWon();
        }
    }
}
