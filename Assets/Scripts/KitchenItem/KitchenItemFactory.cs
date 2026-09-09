using UnityEngine;

public static class KitchenItemFactory
{
    public static KitchenItem CreateFrom(KitchenItemDefinition definition)
    {
        var itemObject = new GameObject(typeof(KitchenItem).Name);
        itemObject.SetActive(false);

        var kitchenItem = itemObject.AddComponent<KitchenItem>();
        kitchenItem.InitializeFrom(definition);

        itemObject.SetActive(true);

        return kitchenItem;
    }

    public static KitchenItem CreateFrom(GameObject prefab)
    {
        var itemObject = Object.Instantiate(prefab);

        return itemObject.GetComponent<KitchenItem>();
    }
}