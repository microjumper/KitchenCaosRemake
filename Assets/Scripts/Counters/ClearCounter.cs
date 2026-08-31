using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class ClearCounter : MonoBehaviour, IInteractable
{
    private IContainer counterContainer;

    private void Awake()
    {
        counterContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer container)
    {
        if (container.IsEmpty && !counterContainer.IsEmpty)
        {
            if (counterContainer.TryRemove(out GameObject item))
            {
                container.TryAdd(item);

                return true;
            }

            return false;
        }

        if (!container.IsEmpty && counterContainer.IsEmpty)
        {
            if (container.TryRemove(out GameObject item))
            {
                counterContainer.TryAdd(item);

                return true;
            }
            return false;
        }

        return false;
    }
}
