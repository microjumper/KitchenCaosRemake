using UnityEngine;

public static class KitchenItemFactory
{
    public static KitchenItem CreateFrom(KitchenItemDefinition definition)
    {
        var itemObject = new GameObject("KitchenItem");
        itemObject.SetActive(false);

        var kitchenItem = itemObject.AddComponent<KitchenItem>();
        kitchenItem.InitializeFrom(definition);

        itemObject.SetActive(true);

        return kitchenItem;
    }
}