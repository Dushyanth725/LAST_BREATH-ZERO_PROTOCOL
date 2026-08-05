using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public MaskEffectManager effectManager;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.P))
        {
            effectManager.WearMask();

            gameObject.SetActive(false);   // Hide the mask after picking it up
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}