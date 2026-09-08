using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.TryRetrieve(out KitchenItem item))
        {
            Destroy(item.gameObject);

            return true;
        }

        return false;
    }
}
