using UnityEngine;

public class ItemPlacePoint : MonoBehaviour
{
    [HideInInspector]
    public ItemPickup currentItem;

    public DrawerInteraction drawer;

    private void Awake()
    {
        if (drawer == null)
            drawer = GetComponentInParent<DrawerInteraction>();
    }

    public bool CanPlaceItem()
    {
        return drawer != null && drawer.IsOpen && currentItem == null;
    }

    public void PlaceItem(ItemPickup item)
    {
        currentItem = item;
    }

    public void RemoveItem()
    {
        currentItem = null;
    }
}