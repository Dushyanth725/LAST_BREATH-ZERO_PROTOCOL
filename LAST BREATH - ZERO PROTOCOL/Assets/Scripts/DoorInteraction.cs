using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    
    public Transform doorPivot;
    public float openAngle = 90f;
    public float speed = 3f;

    bool playerInside = false;
    bool opened = false;
    bool isMoving = false;

    Quaternion closedRot;
    Quaternion openedRot;

    void Start()
    {
        closedRot = doorPivot.localRotation;
        openedRot = closedRot * Quaternion.Euler(0, openAngle, 0);
    }

    public void Interact()
{
    if (!isMoving)
    {
        opened = !opened;
        isMoving = true;
    }
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
}