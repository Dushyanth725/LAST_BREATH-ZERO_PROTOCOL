using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    private ItemPlacePoint currentPlacePoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void PickUp()
    {
        // If the item was stored in a drawer, free that place
        if (currentPlacePoint != null)
        {
            currentPlacePoint.RemoveItem();
            currentPlacePoint = null;
        }

        rb.isKinematic = true;
        rb.useGravity = false;

        col.enabled = false;

        gameObject.SetActive(false);
    }

    public void Drop(Transform dropPoint)
    {
        gameObject.SetActive(true);

        transform.SetParent(null);

        transform.position = dropPoint.position;
        transform.rotation = dropPoint.rotation;

        col.enabled = true;

        rb.isKinematic = false;
        rb.useGravity = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(dropPoint.forward * 2f, ForceMode.Impulse);
    }

    public void Place(ItemPlacePoint placePoint)
    {
        currentPlacePoint = placePoint;

        placePoint.PlaceItem(this);

        gameObject.SetActive(true);

        transform.SetParent(placePoint.transform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        rb.useGravity = false;

        col.enabled = true;
    }
}