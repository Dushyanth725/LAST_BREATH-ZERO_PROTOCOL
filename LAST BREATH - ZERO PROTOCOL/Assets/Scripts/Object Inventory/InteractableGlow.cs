using UnityEngine;

public class InteractableGlow : MonoBehaviour
{
    [Header("Glow")]
    public Material outlineMaterial;

    private Renderer[] renderers;
    private Material[][] originalMaterials;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].sharedMaterials;
        }
    }

    private void Start()
    {
        SetGlow(true);
    }

    public void SetGlow(bool enabled)
    {
        if (outlineMaterial == null)
            return;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (enabled)
            {
                Material[] materials = new Material[originalMaterials[i].Length + 1];

                for (int j = 0; j < originalMaterials[i].Length; j++)
                    materials[j] = originalMaterials[i][j];

                materials[materials.Length - 1] = outlineMaterial;

                renderers[i].materials = materials;
            }
            else
            {
                renderers[i].materials = originalMaterials[i];
            }
        }
    }
}