using UnityEngine;

public abstract class StorePolicy: ScriptableObject
{
    public abstract bool CanStore(KitchenItem item);
}