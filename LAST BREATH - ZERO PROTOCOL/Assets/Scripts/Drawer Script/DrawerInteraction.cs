using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    [Header("Drawer")]
    public Transform drawer;

    [Header("Movement")]
    public float slideDistance = 0.4f;
    public float speed = 3f;
    public Vector3 slideDirection = Vector3.forward;

    private bool opened = false;
    private bool isMoving = false;

    private Vector3 closedPos;
    private Vector3 openedPos;

    private PickupObject[] itemsInside;

    void Start()
    {
        closedPos = drawer.localPosition;
        openedPos = closedPos + slideDirection.normalized * slideDistance;

        // Find every pickup object inside this drawer
        itemsInside = drawer.GetComponentsInChildren<PickupObject>(true);

        // Drawer starts closed
        SetItemsPickup(false);
    }

    public void Interact()
    {
        if (isMoving)
            return;

        opened = !opened;
        isMoving = true;

        SetItemsPickup(opened);
    }

    void Update()
    {
        Vector3 target = opened ? openedPos : closedPos;

        drawer.localPosition = Vector3.MoveTowards(
            drawer.localPosition,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(drawer.localPosition, target) < 0.001f)
        {
            drawer.localPosition = target;
            isMoving = false;
        }
    }

   void SetItemsPickup(bool value)
{
    PickupObject[] items = drawer.GetComponentsInChildren<PickupObject>(true);

    foreach (PickupObject item in items)
    {
        item.canBePickedUp = value;
    }
}
}