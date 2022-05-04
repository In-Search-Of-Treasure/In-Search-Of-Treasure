using Assets.Managers;
using Assets.Managers.Player;
using UnityEngine;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;

    private Button okButton;


    void Start()
    {
        panel.SetActive(false);
        okButton = panel.GetComponentInChildren<Button>();
        okButton.onClick.AddListener(onOkButtonClicked);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.General.Player))
        {
            GameManager.Instance.PauseGame();
            panel.SetActive(true);
        }
    }

    private void onOkButtonClicked()
    {
        GameManager.Instance.ResumeGame();
        PositionManager.Instance.RestartToInitialPosition();
        panel.SetActive(false);
        LifeManager.Instance.RestartLifeNumber();
    }

}
