using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlicebleRecipeRepository", menuName = "ScriptableObjects/SlicebleRecipeRepository")]
public sealed class SlicebleRecipeRepository : RecipeRepository<SliceableItemDefinition>
{
    [SerializeField] private List<SliceableItemDefinition> slicebleItems;

    protected override IReadOnlyList<SliceableItemDefinition> RecipeList => slicebleItems;

    protected override KitchenItemDefinition GetStartingItem(SliceableItemDefinition recipe) => recipe.Input;
}
