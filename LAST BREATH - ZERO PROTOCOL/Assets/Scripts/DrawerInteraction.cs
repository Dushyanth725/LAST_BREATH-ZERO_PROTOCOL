using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    public GameObject prompt;
    public Transform drawer;

    public float slideDistance = 0.4f;
    public float speed = 3f;

    bool playerInside = false;
    bool opened = false;
    bool isMoving = false;

    Vector3 closedPos;
    Vector3 openedPos;

    void Start()
    {
        closedPos = drawer.localPosition;
        openedPos = closedPos - Vector3.forward * slideDistance;

        if (prompt != null)
            prompt.SetActive(false);
    }

    public void ShowPrompt()
    {
        playerInside = true;

        if (prompt != null)
            prompt.SetActive(true);
    }

    public void HidePrompt()
    {
        playerInside = false;

        if (prompt != null)
            prompt.SetActive(false);
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            opened = !opened;
            isMoving = true;
        }

        Vector3 target = opened ? openedPos : closedPos;

        drawer.localPosition = Vector3.MoveTowards(
            drawer.localPosition,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(drawer.localPosition, target) < 0.01f)
        {
            drawer.localPosition = target;
            isMoving = false;
        }
    }
}