using System.Collections.Generic;
using UnityEngine;

public class Deliverable : MonoBehaviour
{
    [SerializeField] private Dictionary<KitchenItemDefinition, GameObject> itemToVisual;

    public IReadOnlyCollection<KitchenItemDefinition> ItemSet => itemSet;

    private readonly HashSet<KitchenItemDefinition> itemSet = new();

    public bool TryAdd(KitchenItemDefinition item)
    {
        if (itemToVisual.TryGetValue(item, out GameObject visual))
        {
            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);

                itemSet.Add(item);

                return true;
            }
        }

        return false;
    }
}
