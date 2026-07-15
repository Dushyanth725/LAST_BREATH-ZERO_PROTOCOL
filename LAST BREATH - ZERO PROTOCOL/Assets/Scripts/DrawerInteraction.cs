using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public GameObject prompt;
    public Transform drawer;

    public float slideDistance = 0.4f;
    public float speed = 3f;

    bool playerInside = false;
    bool opened = false;

    Vector3 closedPos;
    Vector3 openedPos;

    void Start()
    {
        closedPos = drawer.localPosition;

        // Slide forward along local Z
        openedPos = closedPos - Vector3.forward * slideDistance;

        prompt.SetActive(false);
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            opened = !opened;
        }

        Vector3 target = opened ? openedPos : closedPos;

        drawer.localPosition = Vector3.Lerp(
            drawer.localPosition,
            target,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = true;
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInside = false;
            prompt.SetActive(false);
        }
    }
}