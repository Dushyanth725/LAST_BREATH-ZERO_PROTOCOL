using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI")]
    public Image inventoryImage;

    [Header("Drop")]
    public Transform dropPoint;

    private PickupObject heldItem;

    private void Awake()
    {
        Instance = this;

        if (inventoryImage != null)
            inventoryImage.gameObject.SetActive(false);
    }

    public bool IsHoldingItem()
    {
        return heldItem != null;
    }

    public PickupObject GetHeldItem()
    {
        return heldItem;
    }

    public void PickUp(PickupObject item)
    {
        if (item == null)
            return;

        // Switch items if already holding one
        if (heldItem != null)
            DropHeldItem();

        heldItem = item;
        heldItem.PickUp();

        if (inventoryImage != null)
        {
            inventoryImage.sprite = heldItem.itemIcon;
            inventoryImage.gameObject.SetActive(true);
        }
    }

    public void DropHeldItem()
    {
        if (heldItem == null)
            return;

        heldItem.Drop(dropPoint.position, dropPoint.rotation);

        heldItem = null;

        if (inventoryImage != null)
            inventoryImage.gameObject.SetActive(false);
    }

    public bool HoldingKey(string keyID)
{
    if (heldItem == null)
        return false;

    return heldItem.keyID == keyID;
}

}