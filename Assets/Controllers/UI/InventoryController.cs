using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Image Slot1Icon;
    public TMP_Text Slot1Text;
    
    public Image Slot2Icon;
    public TMP_Text Slot2Text;
    
    public Image Slot3Icon;
    public TMP_Text Slot3Text;

    public Sprite fruit1;
    public Sprite fruit2;
    public Sprite fruit3;

    public Button RemoveSlot1;
    public Button RemoveSlot2;
    public Button RemoveSlot3;

    public Button UseSlot1;
    public Button UseSlot2;
    public Button UseSlot3;

    private const string FRUIT1_NAME = "Fruit1";
    private const string FRUIT2_NAME = "Fruit2";
    private const string FRUIT3_NAME = "Fruit3";
    private const string EMPTY = "Empty";

    private void Start()
    {
        RemoveSlot1.onClick.AddListener(OnRemoveSlot1Clicked);
        RemoveSlot2.onClick.AddListener(OnRemoveSlot2Clicked);
        RemoveSlot3.onClick.AddListener(OnRemoveSlot3Clicked);

        UseSlot1.onClick.AddListener(OnUseSlot1Clicked);
        UseSlot2.onClick.AddListener(OnUseSlot2Clicked);
        UseSlot3.onClick.AddListener(OnUseSlot3Clicked);
    }

    private void Update()
    {
        UpdateAllSlots();
    }

    private void UpdateAllSlots()
    {
        UpdateSlot1();
        UpdateSlot2();
        UpdateSlot3();
    }

    private void UpdateSlot1()
    {
        var inventorySlots = InventoryManager.Instance.GetSlots();

        if (inventorySlots.Count > 0)
        {
            ActivateRemoveBtns(1);

            if (inventorySlots != null && inventorySlots[0] == Fruits.Fruit1)
            {
                Slot1Icon.gameObject.SetActive(true);
                Slot1Icon.sprite = fruit1;
                UpdateSlot1Text(FRUIT1_NAME);
            }
            else if (inventorySlots != null && inventorySlots[0] == Fruits.Fruit2)
            {
                Slot2Icon.gameObject.SetActive(true);
                Slot2Icon.sprite = fruit2;
                UpdateSlot1Text(FRUIT2_NAME);
            }
            else if (inventorySlots != null && inventorySlots[0] == Fruits.Fruit3)
            {
                Slot3Icon.gameObject.SetActive(true);
                Slot3Icon.sprite = fruit3;
                UpdateSlot1Text(FRUIT3_NAME);
            }
        }
        else
        {
            RemoveSlot1.interactable = false;
            UseSlot1.interactable = false;
            Slot1Icon.gameObject.SetActive(false);
            UpdateSlot1Text(EMPTY);
        }
    }

    private void UpdateSlot2()
    {
        var inventorySlots = InventoryManager.Instance.GetSlots();

        if (inventorySlots.Count > 1)
        {
            ActivateRemoveBtns(2);

            if (inventorySlots != null && inventorySlots[1] == Fruits.Fruit1)
            {
                Slot1Icon.sprite = fruit1;
                UpdateSlot2Text(FRUIT1_NAME);
            }
            else if (inventorySlots != null && inventorySlots[1] == Fruits.Fruit2)
            {
                Slot2Icon.sprite = fruit2;
                UpdateSlot2Text(FRUIT2_NAME);
            }
            else if (inventorySlots != null && inventorySlots[1] == Fruits.Fruit3)
            {
                Slot3Icon.sprite = fruit3;
                UpdateSlot2Text(FRUIT3_NAME);
            }
        }
        else
        {
            RemoveSlot2.interactable = false;
            UseSlot2.interactable = false;
            Slot2Icon.gameObject.SetActive(false);
            UpdateSlot2Text(EMPTY);
        }
    }

    private void UpdateSlot3()
    {
        var inventorySlots = InventoryManager.Instance.GetSlots();

        if (inventorySlots.Count > 2)
        {
            ActivateRemoveBtns(3);

            if (inventorySlots != null && inventorySlots[2] == Fruits.Fruit1)
            {
                Slot1Icon.sprite = fruit1;
                UpdateSlot3Text(FRUIT1_NAME);
            }
            else if (inventorySlots != null && inventorySlots[2] == Fruits.Fruit2)
            {
                Slot2Icon.sprite = fruit2;
                UpdateSlot3Text(FRUIT2_NAME);
            }
            else if (inventorySlots != null && inventorySlots[2] == Fruits.Fruit3)
            {
                Slot3Icon.sprite = fruit3;
                UpdateSlot3Text(FRUIT3_NAME);
            }
        }
        else
        {
            RemoveSlot3.interactable = false;
            UseSlot3.interactable = false;
            Slot3Icon.gameObject.SetActive(false);
            UpdateSlot3Text(EMPTY);
        }
    }

    private void UpdateSlot1Text(string str)
    {
        Slot1Text.SetText(str);
    }

    private void UpdateSlot2Text(string str)
    {
        Slot2Text.SetText(str);
    }

    private void UpdateSlot3Text(string str)
    {
        Slot3Text.SetText(str);
    }

    private void ActivateRemoveBtns(int btnId)
    {
        switch(btnId){
            case 1:
                RemoveSlot1.interactable = true;
                UseSlot1.interactable = true;
                Slot1Icon.gameObject.SetActive(true);
                break;
            case 2:
                RemoveSlot2.interactable = true;
                UseSlot2.interactable = true;
                Slot2Icon.gameObject.SetActive(true);
                break;
            case 3:
                RemoveSlot3.interactable = true;
                UseSlot3.interactable = true;
                Slot3Icon.gameObject.SetActive(true);
                break;
        }
    }

    private void RemoveItem(int removeButton)
    {
        //When player click on the remove icon

        switch (removeButton)
        {
            case 1:
                InventoryManager.Instance.RemoveFruit(removeButton-1);
                RemoveSlot1.interactable = false;
                UseSlot1.interactable = false;
                Slot1Icon.gameObject.SetActive(false);
                UpdateSlot1Text(EMPTY);
                break;
            case 2:
                InventoryManager.Instance.RemoveFruit(removeButton - 1);
                RemoveSlot2.interactable = false;
                UseSlot2.interactable = false;
                Slot2Icon.gameObject.SetActive(false);
                UpdateSlot1Text(EMPTY);
                break;
            case 3:
                InventoryManager.Instance.RemoveFruit(removeButton - 1);
                RemoveSlot3.interactable = false;
                UseSlot3.interactable = false;
                Slot3Icon.gameObject.SetActive(false);
                UpdateSlot1Text(EMPTY);
                break;
        }       
    }

    private void UseItem(int useButton)
    {
        //When player clicks on the icon
        //Everytime player use some item, we'll need to update
        //all slots.

        //To use an item, we need to access the list on the manager like:
        // InventoryManager.Instance.GetSlots()[usebutton-1]

        Debug.Log($"Player is using Slot {useButton}");
        RemoveItem(useButton);        
    }

    private void OnRemoveSlot1Clicked()
    {
        RemoveItem(1);
    }

    private void OnRemoveSlot2Clicked()
    {
        RemoveItem(2);
    }

    private void OnRemoveSlot3Clicked()
    {
        RemoveItem(3);
    }

    private void OnUseSlot1Clicked()
    {
        UseItem(1);
    }

    private void OnUseSlot2Clicked()
    {
        UseItem(2);
    }

    private void OnUseSlot3Clicked()
    {
        UseItem(3);
    }
}
