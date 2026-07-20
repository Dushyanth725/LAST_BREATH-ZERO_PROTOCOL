using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    DoorInteraction currentDoor;
    DrawerInteraction currentDrawer;
    PickupObject currentPickup;

    void Update()
    {
        // Hide previous prompts every frame
        if (currentDoor != null)
        {
            currentDoor.HidePrompt();
            currentDoor = null;
        }

        if (currentDrawer != null)
        {
            currentDrawer.HidePrompt();
            currentDrawer = null;
        }

        if (currentPickup != null)
        {
            currentPickup.HidePrompt();
            currentPickup = null;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            Debug.Log("Looking at : " + hit.collider.name);
            DoorInteraction door = hit.collider.GetComponent<DoorInteraction>();

            if (door != null)
            {
                currentDoor = door;
                currentDoor.ShowPrompt();
                return;
            }

            DrawerInteraction drawer = hit.collider.GetComponent<DrawerInteraction>();

            if (drawer != null)
            {
                currentDrawer = drawer;
                currentDrawer.ShowPrompt();
                return;
            }

            PickupObject pickup = hit.collider.GetComponentInParent<PickupObject>();

            if (pickup != null)
            {
                currentPickup = pickup;
                currentPickup.ShowPrompt();
                return;
            }
        }
    }
}