using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    public KitchenItemDefinition Definition { get; private set; }

    public void InitializeFrom(KitchenItemDefinition definition)
    {
        Definition = definition;
    }

    private void Awake()
    {
        if (Definition != null)
        {
            Instantiate(Definition.Visual, transform.position, transform.rotation, transform);
        }
    }
}