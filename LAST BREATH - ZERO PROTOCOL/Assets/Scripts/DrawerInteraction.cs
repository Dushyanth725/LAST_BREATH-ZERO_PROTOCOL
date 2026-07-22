using UnityEngine;

public class DrawerInteraction : MonoBehaviour
{
    
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