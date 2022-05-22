using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static List<Fruits> Slots;

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Slots = new List<Fruits>(3);        
    }

    public bool IsFull()
    {
        if (Slots.Count > 2)
            return true;

        return false;
    }

    public bool IsEmpty()
    {
        if (Slots.Count == 0)
            return true;

        return false;
    }

    public void AddFruit(Fruits fruit, GameObject fruitCollided)
    {
        if (IsFull())
        {
            return;
        }          

        Slots.Add(fruit);
        Destroy(fruitCollided);
    }

    public void RemoveFruit(Fruits fruit)
    {
        if (IsEmpty())
            return;

        if (Slots.Contains(fruit))
            Slots.Remove(fruit);
    }

    public void RemoveFruit(int id)
    {
        if (IsEmpty())
            return;

        Slots.RemoveAt(id);
    }

    public List<Fruits> GetSlots()
    {
        return Slots;
    }

    public void RemoveAllFruits()
    {
        if (!IsEmpty())
            Slots.Clear();
    }

    public void UseFruit(Fruits fruit)
    {
        switch (fruit)
        {
            case Fruits.Fruit1:
                //Increase Speed of the Player for 5 seconds.
                StartCoroutine(IncreaseSpeedPlayer());
                return;
            case Fruits.Fruit2:
                //Decrease Speed of All Enemys for 5 seconds.
                StartCoroutine(DecreaseSpeedAllEnemys());
                return;
            case Fruits.Fruit3:
                //Decrease Speed of the Player for 5 seconds.
                StartCoroutine(DecreaseSpeedPlayer());
                return;
        }
    }

    private IEnumerator IncreaseSpeedPlayer()
    {
        PlayerPrefs.SetFloat(PlayerPrefConstants.SkillsRelated.PlayerSpeed, PlayerPrefConstants.SkillsRelated.PlayerIncreaseSpeedValue);

        yield return new WaitForSeconds(5);

        PlayerPrefs.SetFloat(PlayerPrefConstants.SkillsRelated.PlayerSpeed, PlayerPrefConstants.SkillsRelated.PlayerDefaultSpeedValue);
    }

    private IEnumerator DecreaseSpeedAllEnemys()
    {
        PlayerPrefs.SetFloat(PlayerPrefConstants.SkillsRelated.EnemySpeed, PlayerPrefConstants.SkillsRelated.EnemyDecreaseSpeedValue);

        yield return new WaitForSeconds(5);

        PlayerPrefs.SetFloat(PlayerPrefConstants.SkillsRelated.EnemySpeed, PlayerPrefConstants.SkillsRelated.EnemyDefaultSpeedValue);
    }

    private IEnumerator DecreaseSpeedPlayer()
    {
        PlayerPrefs.SetFloat(PlayerPrefConstants.SkillsRelated.PlayerSpeed, PlayerPrefConstants.SkillsRelated.PlayerDecreaseSpeedValue);

        yield return new WaitForSeconds(5);

        PlayerPrefs.SetFloat(PlayerPrefConstants.SkillsRelated.PlayerSpeed, PlayerPrefConstants.SkillsRelated.PlayerDefaultSpeedValue);
    }
}
