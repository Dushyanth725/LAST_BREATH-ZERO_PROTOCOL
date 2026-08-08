using UnityEngine;
using System.Collections;
public class InteractionManager : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    private float keyTextTimer = 0f;

    public GameObject crosshair;
    public GameObject eText;
    public GameObject qText;
    public GameObject rText;
    public GameObject keyNeededText;
  

    private void Start()
    {
        crosshair.SetActive(true);
        eText.SetActive(false);
        qText.SetActive(false);
        rText.SetActive(false);
        keyNeededText.SetActive(false);

        
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
        qText.SetActive(false);
        keyNeededText.SetActive(false);


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
    keyNeededText.SetActive(false);
    keyTextTimer = 0f;

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

    // Showing the "Insert Key" message?
    if (keyTextTimer > 0)
    {
        eText.SetActive(false);
        qText.SetActive(false);
        keyNeededText.SetActive(true);

        keyTextTimer -= Time.deltaTime;

        if (keyTextTimer <= 0f)
        {
            keyNeededText.SetActive(false);
            eText.SetActive(true);
        }

        return;
    }

    eText.SetActive(true);

    if (Input.GetKeyDown(KeyCode.E))
    {
        if (door.IsLocked())
        {
            if (InventoryManager.Instance.HoldingKey(door.keyID))
            {
                door.Unlock();
                door.Interact();
            }
            else
            {
                keyNeededText.SetActive(true);
                eText.SetActive(false);
                keyTextTimer = 2f;
            }
        }
        else
        {
            door.Interact();
        }
    }

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
// GAS MASK HOTKEY
//==================================================

if (Input.GetKeyDown(KeyCode.R))
{
    Debug.Log("Global R");

    GasMask gasMask = GasMask.Instance;

    if (gasMask == null)
    {
        Debug.Log("GasMask NULL");
        return;
    }

    Debug.Log("Wearing = " + gasMask.isWearing);
    Debug.Log("Inventory = " + gasMask.inInventory);

    if (gasMask.isWearing)
    {
        Debug.Log("Removing Mask");

        gasMask.RemoveToInventory();
        InventoryManager.Instance.StoreMask(gasMask);

        return;
    }

    if (gasMask.inInventory)
    {
        Debug.Log("Wear Again");

        gasMask.Wear();
        InventoryManager.Instance.RemoveStoredMask();

        return;
    }
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

   GasMask gasMask = item.GetComponent<GasMask>();
   Debug.Log("GasMask Component = " + gasMask);

if (gasMask != null)
{
    crosshair.SetActive(false);
    rText.SetActive(true);

    if (Input.GetKeyDown(KeyCode.R))
    {
        // On floor -> Wear
        if (!gasMask.isWearing && !gasMask.inInventory)
        {
            gasMask.Wear();

            // Hide R after wearing
            rText.SetActive(false);
            crosshair.SetActive(true);
        }

        // Wearing -> Inventory
        else if (gasMask.isWearing)
        {
            gasMask.RemoveToInventory();

            InventoryManager.Instance.StoreMask(gasMask);

            rText.SetActive(false);
            crosshair.SetActive(true);
        }

        // Inventory -> Wear again
        else if (gasMask.inInventory)
        {
            gasMask.Wear();

            InventoryManager.Instance.RemoveStoredMask();

            rText.SetActive(false);
            crosshair.SetActive(true);
        }
    }

    return;
}

    // NORMAL PICKUP
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