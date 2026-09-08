using UnityEngine;

public class SupplyCounter : MonoBehaviour, IInteractable
{
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    [SerializeField] private KitchenItemDefinition supply;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer doorRender;

    private void Start()
    {
        doorRender.sprite = supply.Sprite;
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.HasItem)
        {
            return false;
        }

        animator.SetTrigger(OpenClose);

        var item = KitchenItemFactory<Ingredient>.CreateFrom(supply);

        return otherContainer.TryStore(item);
    }
}
