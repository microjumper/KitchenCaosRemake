using UnityEngine;

public static class KitchenItemFactory<T> where T : KitchenItem
{
    public static T CreateFrom(KitchenItemDefinition definition)
    {
        var itemObject = new GameObject(typeof(T).Name);
        itemObject.SetActive(false);

        var kitchenItem = itemObject.AddComponent<T>();
        kitchenItem.InitializeFrom(definition);

        itemObject.SetActive(true);

        return kitchenItem;
    }
}