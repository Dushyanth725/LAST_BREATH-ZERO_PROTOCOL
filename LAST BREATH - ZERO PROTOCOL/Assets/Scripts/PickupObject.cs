using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject prompt;

    Rigidbody rb;
    Collider col;

    bool isHeld = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        if (prompt != null)
            prompt.SetActive(false);
    }

    public void ShowPrompt()
    {
        if (!isHeld && prompt != null)
            prompt.SetActive(true);
    }

    public void HidePrompt()
    {
        if (prompt != null)
            prompt.SetActive(false);
    }

    public void PickUp()
    {
        isHeld = true;

        if (prompt != null)
            prompt.SetActive(false);

        gameObject.SetActive(false);
    }

    public void Drop(Vector3 position)
    {
        transform.position = position;

        gameObject.SetActive(true);

        rb.isKinematic = false;
        rb.useGravity = true;

        if (col != null)
            col.enabled = true;

        isHeld = false;
    }

    void Update()
    {
        if (isHeld)
            return;

        if (prompt != null && prompt.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            PickupManager.Instance.Pickup(this);
        }
    }
}