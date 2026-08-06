using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Door")]
    public Transform doorPivot;

    [Header("Items Inside")]
    public Transform vaultMesh;

    [Header("Lock")]
    public bool requiresKey = false;
    public string keyID = "";

    public float openAngle = 90f;
    public float speed = 3f;

    bool opened = false;
    bool isMoving = false;

    Quaternion closedRot;
    Quaternion openedRot;

    void Start()
    {
        closedRot = doorPivot.localRotation;
        openedRot = closedRot * Quaternion.Euler(0, openAngle, 0);

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
        Quaternion target = opened ? openedRot : closedRot;

        doorPivot.localRotation = Quaternion.RotateTowards(
            doorPivot.localRotation,
            target,
            speed * 100f * Time.deltaTime);

        if (Quaternion.Angle(doorPivot.localRotation, target) < 0.5f)
        {
            doorPivot.localRotation = target;
            isMoving = false;
        }
    }

    void SetItemsPickup(bool value)
    {
        if (vaultMesh == null)
            return;

        PickupObject[] items = vaultMesh.GetComponentsInChildren<PickupObject>(true);

        foreach (PickupObject item in items)
        {
            item.canBePickedUp = value;
        }
    }
}