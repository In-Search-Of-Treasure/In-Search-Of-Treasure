using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    private const string LIFE_KEY = "Life";

    public static LifeManager Instance { get; private set; }
    public Text LifeUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        UpdateLifeNumber();
    }

    public void DecrementLife()
    {
        var newLifeNumber = GetLifeNumber() - 1;
        PlayerPrefs.SetInt(LIFE_KEY, newLifeNumber);
        UpdateLifeNumber();
    }

    public int GetLifeNumber()
    {
        var lifeNumber = PlayerPrefs.GetInt(LIFE_KEY, 5);

        return lifeNumber;
    }

    public void RestartLifeNumber()
    {
        PlayerPrefs.SetInt(LIFE_KEY, 5);
        UpdateLifeNumber();
    }

    private void UpdateLifeNumber()
    {
        LifeUI.text = $" {GetLifeNumber()}";
    }
}