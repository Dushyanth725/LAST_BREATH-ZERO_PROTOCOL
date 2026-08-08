using UnityEngine;

public class GasMask : MonoBehaviour
{
    public MaskEffectManager effectManager;
    public static GasMask Instance;

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

         Debug.Log("Calling MaskEffect");

        effectManager.WearMask();

        pickup.PickUp();
    }

    public void RemoveToInventory()
    {
        isWearing = false;
        inInventory = true;

        effectManager.RemoveMask();
    }

    public void DropFromInventory(Vector3 pos, Quaternion rot)
    {
        isWearing = false;
        inInventory = false;

        pickup.Drop(pos, rot);
    }
}