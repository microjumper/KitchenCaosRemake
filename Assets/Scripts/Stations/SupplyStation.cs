using UnityEngine;

public class SupplyStation : MonoBehaviour, IInteractable
{
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    [SerializeField] private KitchenItemDefinition supply;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer doorRender;

    private void Start()
    {
        doorRender.sprite = supply.Sprite;
    }

    public bool TryInteractWith(IContainer container)
    {
        if (container.IsEmpty)
        {
            animator.SetTrigger(OpenClose);

            var item = KitchenItemFactory.CreateFrom(supply);

            return container.TryAdd(item.gameObject);
        }

        return false;
    }
}
