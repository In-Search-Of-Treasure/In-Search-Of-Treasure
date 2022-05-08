using TMPro;
using UnityEngine;

public class HudController : Observer
{
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject Heart4;
    public GameObject Heart5;

    public TextMeshProUGUI LevelName;

    private int LifeNumber => LifeManager.Instance.GetLifeNumber();

    private void Start()
    {
        SetCurrentLevelName();
    }

    private void DecrementHeart()
    {
        switch (LifeNumber)
        {
            case 5:
                Heart5.SetActive(false);
                break;
            case 4:
                Heart4.SetActive(false);
                break;
            case 3:
                Heart3.SetActive(false);
                break;
            case 2:
                Heart2.SetActive(false);
                break;
            default:
                Heart1.SetActive(false);
                break;
        }

        LifeManager.Instance.DecrementLife();
    }

    private void SetCurrentLevelName()
    {
        LevelName.text = SceneGameManager.Instance.GetCurrentSceneName();
    }

    public override void OnNotify(object value, NotificationType notificationType)
    {
        if(notificationType == NotificationType.PlayerLost1Life)
        {
            DecrementHeart();            
        }
    }
}
