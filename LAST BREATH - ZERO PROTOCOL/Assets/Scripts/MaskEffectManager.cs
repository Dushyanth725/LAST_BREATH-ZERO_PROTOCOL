using UnityEngine;

public class MaskEffectManager : MonoBehaviour
{
    public GameObject normalVolume;
    public GameObject maskVolume;

    private bool wearingMask = false;

    public bool WearingMask => wearingMask;

    public void WearMask()
    {
         Debug.Log("Inside WearMask()");
        if (wearingMask)
            return;

        wearingMask = true;

        normalVolume.SetActive(false);
        maskVolume.SetActive(true);
    }

    public void RemoveMask()
    {
        if (!wearingMask)
            return;
        Debug.Log("WearMask() called");
        wearingMask = false;

        normalVolume.SetActive(true);
        maskVolume.SetActive(false);
    }
}