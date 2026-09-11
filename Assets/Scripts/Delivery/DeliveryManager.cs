using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private List<MenuItemDefinition> menuItems;

    private void OnEnable()
    {
        deliveryCounter.Delivered += OnDelivered;
    }

    private void OnDisable()
    {
        deliveryCounter.Delivered -= OnDelivered;
    }

    private void OnDelivered(ImmutableHashSet<KitchenItemDefinition> preparedIngredients)
    {
        Debug.Log(IsCorrectRecipe(preparedIngredients));
    }

    private bool IsCorrectRecipe(ImmutableHashSet<KitchenItemDefinition> preparedIngredients)
    {
        foreach (var item in menuItems)
        {
            if (preparedIngredients.Count == item.Ingredients.Count)
            {
                if (preparedIngredients.SetEquals(item.Ingredients))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
