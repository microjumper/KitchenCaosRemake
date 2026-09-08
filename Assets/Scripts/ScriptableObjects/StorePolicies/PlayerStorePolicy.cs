using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStorePolicy", menuName = "ScriptableObjects/Store Policy/PlayerStorePolicy")]
public class PlayerStorePolicy : StorePolicy
{
    public override bool CanStore(KitchenItem item)
    {
        return item is Ingredient || item is Plate;
    }
}