using UnityEngine;

public class SupplyStation : MonoBehaviour, IInteractable
{
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer doorRender;
    [SerializeField] KitchenItemDefinition supply;

    private void Start()
    {
        doorRender.sprite = supply.Sprite;
    }

    public bool TryInteractWith(IContainer container)
    {
        if (container.IsEmpty)
        {
            animator.SetTrigger(OpenClose);

            GameObject item = Instantiate(supply.Visual);

            return container.TryAdd(item);
        }

        return false;
    }
}
