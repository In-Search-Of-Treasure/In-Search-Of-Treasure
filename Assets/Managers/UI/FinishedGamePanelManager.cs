using UnityEngine;

public class FinishedGamePanelManager : MonoBehaviour
{
    public static FinishedGamePanelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }


}
