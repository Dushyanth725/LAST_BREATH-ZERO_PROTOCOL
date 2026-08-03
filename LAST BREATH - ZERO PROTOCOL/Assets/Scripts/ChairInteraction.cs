using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChairInteraction : MonoBehaviour
{
    public static ChairInteraction CurrentChair;

    [Header("References")]
    public Transform player;

    [Header("Settings")]
    public float holdDistance = 1.2f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;
    public float rotationOffset = -90f;

    private bool isHeld = false;
    private Collider chairCollider;
    private CharacterController playerController;

    public bool IsHeld => isHeld;

    private void Start()
    {
        chairCollider = GetComponent<Collider>();
        playerController = player.GetComponent<CharacterController>();
    }

    public void Interact()
    {
        if (isHeld)
        {
            Release();
            return;
        }

        isHeld = true;
        CurrentChair = this;

        if (chairCollider != null && playerController != null)
            Physics.IgnoreCollision(chairCollider, playerController, true);
    }

    public void Release()
    {
        isHeld = false;

        if (CurrentChair == this)
            CurrentChair = null;

        if (chairCollider != null && playerController != null)
            Physics.IgnoreCollision(chairCollider, playerController, false);
    }

    public Vector3 GetTargetPosition()
    {
        Vector3 pos = player.position + player.forward * holdDistance;
        pos.y = transform.position.y;
        return pos;
    }

    public Quaternion GetTargetRotation()
    {
        return Quaternion.Euler(
            0f,
            player.eulerAngles.y + rotationOffset,
            0f);
    }
}