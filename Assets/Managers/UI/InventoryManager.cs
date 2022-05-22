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

}
