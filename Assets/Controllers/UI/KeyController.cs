using Assets.Managers;
using UnityEngine;

public class KeyController : Observer
{
    private bool InventoryIsOpen;

    public override void OnNotify(NotificationType notificationType, object value = null)
    {

        if (notificationType == NotificationType.CutsceneSkipped)
        {
            CutsceneSkipped();
        }

        if (notificationType == NotificationType.PlayerOpenOrCloseInventory)
        {
            InventoryPressed(value);
        }
    }

    private void InventoryPressed(object value)
    {
        GameObject inventory;

        if (value != null)
        {
            inventory = (GameObject)value;

            OpenCloseInventory(inventory);
        }
    }

    private void CutsceneSkipped()
    {
        SceneGameManager.Instance.SetScene(SceneConstants.Demo);
    }

    private void OpenInventory(GameObject inventory)
    {
        inventory.SetActive(true);
    }

    private void CloseInventory(GameObject inventory)
    {
        inventory.SetActive(false);
    }

    private void OpenCloseInventory(GameObject inventory)
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}
