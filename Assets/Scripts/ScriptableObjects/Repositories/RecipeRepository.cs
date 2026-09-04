using System.Collections.Generic;
using UnityEngine;

public abstract class RecipeRepository<TRecipe> : ScriptableObject, IRepository<KitchenItemDefinition, TRecipe>
{
    protected abstract IReadOnlyList<TRecipe> RecipeList { get; }
    protected abstract KitchenItemDefinition GetStartingItem(TRecipe recipe);

    private Dictionary<KitchenItemDefinition, TRecipe> recipes;

    protected virtual void OnEnable()
    {
        recipes = new Dictionary<KitchenItemDefinition, TRecipe>();

        if (RecipeList == null)
        {
            return;
        }

        foreach (var recipe in RecipeList)
        {
            var startingItem = GetStartingItem(recipe);

            if (startingItem == null)
            {
                continue;
            }

            recipes.TryAdd(startingItem, recipe);
        }
    }

    public bool TryGet(KitchenItemDefinition startingItem, out TRecipe recipe)
    {
        if (recipes == null)
        {
            recipe = default;

            return false;
        }

        return recipes.TryGetValue(startingItem, out recipe);
    }
}