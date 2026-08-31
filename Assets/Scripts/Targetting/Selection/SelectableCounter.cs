using UnityEngine;

public class SelectableCounter : MonoBehaviour, ISelectable
{
    private static readonly int EmissionColorId = Shader.PropertyToID("_EmissionColor");

    [SerializeField] private Color emissionColor = new(0.25f, 0.25f, 0.25f);
    [SerializeField] private MeshRenderer[] renderers;

    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    public void Select() => SetEmission(emissionColor);

    public void Deselect() => SetEmission(Color.black);

    private void SetEmission(Color color)
    {
        foreach (var renderer in renderers)
        {
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor(EmissionColorId, color);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
