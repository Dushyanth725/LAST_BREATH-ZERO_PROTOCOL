using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour
{
    [Header("Inventory")]
    public Sprite itemIcon;

    [HideInInspector]
    public bool canBePickedUp = true;

    private Rigidbody rb;
    private Collider[] colliders;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

   public void PickUp()
{
    transform.SetParent(null, true);

    canBePickedUp = true;

    if (rb != null)
    {
        // Make sure physics is enabled before clearing velocities
        rb.isKinematic = false;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    gameObject.SetActive(false);
}

   public void Drop(Vector3 position, Quaternion rotation)
{
    transform.position = position;
    transform.rotation = rotation;

    gameObject.SetActive(true);

    if (rb != null)
    {
        rb.isKinematic = false;
        rb.useGravity = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Gentle forward throw
        rb.AddForce(Camera.main.transform.forward * 0.6f, ForceMode.Impulse);

        // Very slight natural flip
        rb.AddTorque(Camera.main.transform.right * 0.15f, ForceMode.Impulse);
    }
}
private void EnablePickup()
{
    canBePickedUp = true;
}
    IEnumerator EnablePickupDelay()
    {
        canBePickedUp = false;

        yield return new WaitForSeconds(0.25f);

        canBePickedUp = true;
    }
}