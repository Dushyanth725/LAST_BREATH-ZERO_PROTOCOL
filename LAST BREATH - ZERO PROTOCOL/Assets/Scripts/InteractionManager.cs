using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    public GameObject crosshair;
    public GameObject eText;
    public GameObject qText;

    private void Start()
    {
        crosshair.SetActive(true);
        eText.SetActive(false);
        qText.SetActive(false);
    }

    private void Update()
    {
        crosshair.SetActive(true);
        eText.SetActive(false);
        qText.SetActive(false);

        // If holding an item, allow dropping anywhere with Q
       if (InventoryManager.Instance != null && InventoryManager.Instance.IsHoldingItem())
{
    if (Input.GetKeyDown(KeyCode.Q))
    {
        InventoryManager.Instance.DropHeldItem();
        return;
    }
}

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, interactDistance, interactLayer))
            return;
        Debug.Log("Hit : " + hit.collider.name);
        // Door
        DoorInteraction door = hit.collider.GetComponent<DoorInteraction>();
        if (door != null)
        {
            crosshair.SetActive(false);
            eText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                door.Interact();

            return;
        }

        // Drawer
        DrawerInteraction drawer = hit.collider.GetComponent<DrawerInteraction>();
        if (drawer != null)
        {
            crosshair.SetActive(false);
            eText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                drawer.Interact();

            return;
        }

        // Pickup Item
        ItemPickup item = hit.collider.GetComponentInParent<ItemPickup>();
        if (item != null)
        {
            crosshair.SetActive(false);
            qText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                InventoryManager.Instance.PickUp(item);
            }

            return;
        }
    }
}