using UnityEngine;

public interface IItemHolder
{
    bool CanHold(GameObject item);
    void Hold(GameObject item);
    GameObject Release();
}
