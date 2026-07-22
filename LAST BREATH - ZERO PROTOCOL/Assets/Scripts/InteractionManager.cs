using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    public GameObject crosshair;
    public GameObject eText;
    public GameObject qText;

    void Start()
    {
        crosshair.SetActive(true);
        eText.SetActive(false);
        qText.SetActive(false);
    }

    void Update()
    {
        crosshair.SetActive(true);
        eText.SetActive(false);
        qText.SetActive(false);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            DoorInteraction door = hit.collider.GetComponent<DoorInteraction>();

            if (door != null)
            {
                crosshair.SetActive(false);
                eText.SetActive(true);
                qText.SetActive(false);

                if (Input.GetKeyDown(KeyCode.E))
                    door.Interact();

                return;
            }

            DrawerInteraction drawer = hit.collider.GetComponent<DrawerInteraction>();

            if (drawer != null)
            {
                crosshair.SetActive(false);
                eText.SetActive(true);
                qText.SetActive(false);

                if (Input.GetKeyDown(KeyCode.E))
                    drawer.Interact();

                return;
            }

            PickupItem pickup = hit.collider.GetComponentInParent<PickupItem>();

            if (pickup != null)
            {
                crosshair.SetActive(false);
                eText.SetActive(false);
                qText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Q))
                    pickup.Interact();

                return;
            }
        }
    }
}