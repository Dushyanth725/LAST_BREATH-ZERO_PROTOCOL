using UnityEngine;

public class MaskEffectManager : MonoBehaviour
{
    public GameObject normalVolume;
    public GameObject maskVolume;

    private bool wearingMask = false;

    public void WearMask()
    {
        if (wearingMask) return;

        wearingMask = true;

        normalVolume.SetActive(false);
        maskVolume.SetActive(true);

        Debug.Log("Mask Equipped");
    }
}