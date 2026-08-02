using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI")]
    public Image inventoryImage;

    [Header("Drop Settings")]
    public Transform dropPoint;

    private ItemPickup heldItem;

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

    public void PickUp(ItemPickup item)
    {
        if (heldItem != null)
            return;

        heldItem = item;

        heldItem.PickUp();

       if (inventoryImage != null)
    inventoryImage.gameObject.SetActive(true);
    }

    private bool TryPlaceItem()
{
    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));

    RaycastHit hit;

    if (!Physics.Raycast(ray, out hit, 3f))
        return false;

    DrawerInteraction drawer = hit.collider.GetComponent<DrawerInteraction>();

    if (drawer == null)
        drawer = hit.collider.GetComponentInParent<DrawerInteraction>();

    if (drawer == null)
        return false;

    if (!drawer.IsOpen)
        return false;

   ItemPlacePoint[] placePoints = FindObjectsOfType<ItemPlacePoint>();

ItemPlacePoint placePoint = null;

foreach (ItemPlacePoint p in placePoints)
{
    if (p.drawer == drawer)
    {
        placePoint = p;
        break;
    }
}

    if (placePoint == null)
        return false;

    if (!placePoint.CanPlaceItem())
        return false;

    heldItem.Place(placePoint);

    heldItem = null;

    if (inventoryImage != null)
        inventoryImage.gameObject.SetActive(false);

    return true;
}

   public void DropHeldItem()
{
    if (heldItem == null)
        return;

    if (TryPlaceItem())
        return;

    heldItem.Drop(dropPoint);

    heldItem = null;

    if (inventoryImage != null)
        inventoryImage.gameObject.SetActive(false);
}
}