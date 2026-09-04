using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlicebleRecipeRepository", menuName = "ScriptableObjects/SlicebleRecipeRepository")]
public sealed class SlicebleRecipeRepository : RecipeRepository<SlicebleItemDefinition>
{
    [SerializeField] private List<SlicebleItemDefinition> slicebleItems;

    protected override IReadOnlyList<SlicebleItemDefinition> RecipeList => slicebleItems;

    protected override KitchenItemDefinition GetStartingItem(SlicebleItemDefinition recipe) => recipe.Input;
}
