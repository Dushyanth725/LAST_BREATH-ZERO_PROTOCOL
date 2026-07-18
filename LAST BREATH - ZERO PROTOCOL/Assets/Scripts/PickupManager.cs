using UnityEngine;
using UnityEngine.UI;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance;

    public Image heldItemImage;
    public Transform dropPoint;

    private PickupObject heldItem;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && heldItem != null)
        {
            heldItem.Drop(dropPoint.position);

            heldItem = null;

            if (heldItemImage != null)
                heldItemImage.gameObject.SetActive(false);
        }
    }

    public bool IsHoldingItem()
    {
        return heldItem != null;
    }

    public void Pickup(PickupObject item)
    {
        if (heldItem != null)
            return;

        heldItem = item;

        heldItem.PickUp();

        if (heldItemImage != null)
            heldItemImage.gameObject.SetActive(true);
    }
}