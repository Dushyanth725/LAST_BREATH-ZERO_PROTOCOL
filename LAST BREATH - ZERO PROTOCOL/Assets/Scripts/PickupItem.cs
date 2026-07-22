using UnityEngine;

public class PickupItem : MonoBehaviour
{
   
    public GameObject handModel;

    void Start()
    {

        if (handModel != null)
            handModel.SetActive(false);
    }

    
public void Interact()
{
    Debug.Log("Pickup called");
    InventoryManager.Instance.PickUp(this);
}
}