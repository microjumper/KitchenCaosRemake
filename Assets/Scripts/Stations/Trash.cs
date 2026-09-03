using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.IsEmpty)
        {
            return false;
        }

        if (otherContainer.TryRemove(out GameObject item))
        {
            Destroy(item);
            
            return true;
        }

        return false;
    }
}
