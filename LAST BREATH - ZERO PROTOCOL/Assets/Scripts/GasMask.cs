using UnityEngine;

public class GasMask : MonoBehaviour
{
    public static GasMask Instance;

    public MaskEffectManager effectManager;

    private PickupObject pickup;

    public bool isWearing = false;
    public bool inInventory = false;

    private void Awake()
    {
        Instance = this;
        pickup = GetComponent<PickupObject>();
    }

    public void Wear()
    {
        Debug.Log("GasMask Wear()");

        isWearing = true;
        inInventory = false;

        effectManager.WearMask();

        pickup.PickUp();
    }

    public void RemoveToInventory()
    {
        Debug.Log("GasMask RemoveToInventory()");

        isWearing = false;
        inInventory = true;

        effectManager.RemoveMask();
    }

    public void DropFromInventory(Vector3 position, Quaternion rotation)
    {
        isWearing = false;
        inInventory = false;

        pickup.Drop(position, rotation);
    }
}