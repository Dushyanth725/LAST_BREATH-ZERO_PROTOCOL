using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Image itemImage;
    public Transform dropPoint;

    private PickupItem currentItem;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (currentItem != null && Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    public void PickUp(PickupItem item)
    {
        if (currentItem != null)
            return;

        currentItem = item;

        item.gameObject.SetActive(false);

        if (itemImage != null)
            itemImage.gameObject.SetActive(true);
    }

    void DropItem()
    {
        
        currentItem.transform.SetParent(null);
        currentItem.transform.position = dropPoint.position;
        currentItem.gameObject.SetActive(true);
        currentItem.transform.position = dropPoint.position;
        Debug.Log("Dropped at: " + dropPoint.position);

        Rigidbody rb = currentItem.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = true;
        }

        if (itemImage != null)
            itemImage.gameObject.SetActive(false);

        currentItem = null;
    }
}