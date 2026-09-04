using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookableRecipeRepository", menuName = "ScriptableObjects/CookableRecipeRepository")]
public sealed class CookableRecipeRepository : RecipeRepository<CookableItemDefinition>
{     
    [SerializeField] private List<CookableItemDefinition> cookableItems;

    protected override IReadOnlyList<CookableItemDefinition> RecipeList => cookableItems;
    
    protected override KitchenItemDefinition GetStartingItem(CookableItemDefinition recipe) => recipe.Raw;
}
