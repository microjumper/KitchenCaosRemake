using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlicingRecipeRepository", menuName = "ScriptableObjects/SlicingRecipeRepository")]
public class SlicingRecipeRepository : ScriptableObject, IRepository<KitchenItemDefinition, KitchenItemDefinition>
{
    [SerializeField] private List<SlicebleItemDefinition> slicebleItems;

    private Dictionary<KitchenItemDefinition, KitchenItemDefinition> repository;

    private void OnEnable()
    {
        repository = new Dictionary<KitchenItemDefinition, KitchenItemDefinition>();

        foreach (var pair in slicebleItems)
        {
            repository.Add(pair.Input, pair.Output);
        }
    }

    public bool TryGet(KitchenItemDefinition whole, out KitchenItemDefinition sliced) => repository.TryGetValue(whole, out sliced);
}
