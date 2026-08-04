using UnityEngine;

public class DoubleDoorInteraction : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openAngle = 90f;
    public float speed = 3f;

    bool opened = false;
    bool isMoving = false;

    Quaternion leftClosed;
    Quaternion leftOpen;

    Quaternion rightClosed;
    Quaternion rightOpen;

    void Start()
    {
        leftClosed = leftDoor.localRotation;
        rightClosed = rightDoor.localRotation;

        leftOpen = leftClosed * Quaternion.Euler(0, -openAngle, 0);
        rightOpen = rightClosed * Quaternion.Euler(0, openAngle, 0);
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
        Quaternion leftTarget = opened ? leftOpen : leftClosed;
        Quaternion rightTarget = opened ? rightOpen : rightClosed;

        leftDoor.localRotation = Quaternion.RotateTowards(
            leftDoor.localRotation,
            leftTarget,
            speed * 100f * Time.deltaTime);

        rightDoor.localRotation = Quaternion.RotateTowards(
            rightDoor.localRotation,
            rightTarget,
            speed * 100f * Time.deltaTime);

        if (Quaternion.Angle(leftDoor.localRotation, leftTarget) < 0.5f &&
            Quaternion.Angle(rightDoor.localRotation, rightTarget) < 0.5f)
        {
            leftDoor.localRotation = leftTarget;
            rightDoor.localRotation = rightTarget;
            isMoving = false;
        }
    }
}