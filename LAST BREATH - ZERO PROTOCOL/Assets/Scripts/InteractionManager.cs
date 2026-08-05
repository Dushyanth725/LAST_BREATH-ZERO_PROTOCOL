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
        //==================================================
        // CHAIR IS BEING HELD
        //==================================================

        if (ChairInteraction.CurrentChair != null)
        {
            crosshair.SetActive(false);
            eText.SetActive(true);
            qText.SetActive(false);

           

            if (Input.GetKeyDown(KeyCode.E))
            {
                ChairInteraction.CurrentChair.Release();
            }

            return;
        }

        //==================================================
        // NORMAL UI
        //==================================================

        crosshair.SetActive(true);
        eText.SetActive(false);
        qText.SetActive(false);


        //==================================================
        // RAYCAST
        //==================================================

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //==================================================
        // NOTHING HIT
        //==================================================

        if (!Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if (InventoryManager.Instance != null &&
                InventoryManager.Instance.IsHoldingItem() &&
                Input.GetKeyDown(KeyCode.Q))
            {
                InventoryManager.Instance.DropHeldItem();
            }

            return;
        }

        Debug.Log("Hit : " + hit.collider.name);

        //==================================================
        // SINGLE DOOR
        //==================================================

        DoorInteraction door = hit.collider.GetComponent<DoorInteraction>();

        if (door != null)
        {
            crosshair.SetActive(false);
            eText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                door.Interact();

            return;
        }

        //==================================================
        // DOUBLE DOOR
        //==================================================

        DoubleDoorInteraction doubleDoor = hit.collider.GetComponent<DoubleDoorInteraction>();

        if (doubleDoor != null)
        {
            crosshair.SetActive(false);
            eText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                doubleDoor.Interact();

            return;
        }

        //==================================================
        // DRAWER
        //==================================================

        DrawerInteraction drawer = hit.collider.GetComponent<DrawerInteraction>();

        if (drawer != null)
        {
            crosshair.SetActive(false);
            eText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                drawer.Interact();

            return;
        }

        //==================================================
        // CHAIR
        //==================================================

        ChairInteraction chair = hit.collider.GetComponent<ChairInteraction>();

        if (chair != null)
        {
            crosshair.SetActive(false);
            eText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                chair.Interact();
            }

            return;
        }
//==================================================
// PICKUP ITEM
//==================================================

PickupObject item = hit.collider.GetComponentInParent<PickupObject>();

if (item != null)
{
    if (!item.canBePickedUp)
        return;

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