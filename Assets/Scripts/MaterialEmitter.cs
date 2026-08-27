using UnityEngine;

public class MaterialEmitter : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private Material material;

    private void Awake()
    {
        material = meshRenderer.material;
    }

    private void Start()
    {
        Emit();
    }

    private void Emit()
    {
        material.EnableKeyword("_EMISSION");
        Color emissionColor = new (0.25f, 0.25f, 0.25f);
        material.SetColor("_EmissionColor", emissionColor);
    }
}
