using UnityEngine;

public class SelectionHandler : MonoBehaviour, ISelectable
{
    private static readonly int EmissionColorId = Shader.PropertyToID("_EmissionColor");

    [SerializeField] private Color emissionColor = new(0.25f, 0.25f, 0.25f);

    private Renderer targetRenderer;
    private MaterialPropertyBlock propertyBlock;

    private void Awake()
    {
        targetRenderer = GetComponentInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    public void Select()
    {
        targetRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(EmissionColorId, emissionColor);
        targetRenderer.SetPropertyBlock(propertyBlock);
    }

    public void Deselect()
    {
        targetRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(EmissionColorId, Color.black);
        targetRenderer.SetPropertyBlock(propertyBlock);
    }
}
