using System.Collections.Generic;
using UnityEngine;

public class Deliverable : MonoBehaviour
{
    [SerializeField] private List<SerializableKeyValuePair<KitchenItemDefinition, GameObject>> itemToVisualPairs;

    private Dictionary<KitchenItemDefinition, GameObject> itemToVisual;

    private void Awake()
    {
        itemToVisual = itemToVisualPairs.ToDictionary();
    }

    public bool TryAdd(KitchenItemDefinition item)
    {
        if (itemToVisual.TryGetValue(item, out GameObject visual))
        {
            if (!visual.activeInHierarchy)
            {
                visual.SetActive(true);

                return true;
            }
        }

        return false;
    }
}
