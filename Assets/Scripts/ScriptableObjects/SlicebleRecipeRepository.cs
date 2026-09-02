using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlicebleRecipeRepository", menuName = "ScriptableObjects/SlicebleRecipeRepository")]
public class SlicebleRecipeRepository : ScriptableObject, IRepository<KitchenItemDefinition, SlicebleItemDefinition>
{
    [SerializeField] private List<SlicebleItemDefinition> slicebleItems;

    private Dictionary<KitchenItemDefinition, SlicebleItemDefinition> repository;

    private void OnEnable()
    {
        repository = new Dictionary<KitchenItemDefinition, SlicebleItemDefinition>();

        foreach (var recipe in slicebleItems)
        {
            repository.Add(recipe.Input, recipe);
        }
    }

    public bool TryGet(KitchenItemDefinition whole, out SlicebleItemDefinition recipe) => repository.TryGetValue(whole, out recipe);
}
