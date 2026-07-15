using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject prompt;       // E image
    public Transform doorPivot;     // Door_controller
    public float openAngle = 90f;
    public float speed = 3f;

    bool playerInside = false;
    bool opened = false;

    Quaternion closedRot;
    Quaternion openedRot;

    void Start()
    {
        closedRot = doorPivot.localRotation;
        openedRot = closedRot * Quaternion.Euler(0, openAngle, 0);

        prompt.SetActive(false);
    }

    void Update()
    {
        if(playerInside)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                opened = !opened;
            }
        }

        if(opened)
            doorPivot.localRotation = Quaternion.Slerp(
                doorPivot.localRotation,
                openedRot,
                speed * Time.deltaTime);
        else
            doorPivot.localRotation = Quaternion.Slerp(
                doorPivot.localRotation,
                closedRot,
                speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = true;
            prompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = false;
            prompt.SetActive(false);
        }
    }
}