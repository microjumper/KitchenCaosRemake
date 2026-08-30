using UnityEngine;

public class CounterTop : MonoBehaviour, IItemHolder
{
    private GameObject item = null;

    public bool CanHold(GameObject item)
    {
        return this.item == null;
    }

    public void Hold(GameObject item)
    {
        this.item = Instantiate(item, transform.position, transform.rotation, transform);
    }

    public GameObject Release()
    {
        GameObject releasedItem = item;
        releasedItem.transform.SetParent(null);
        item = null;
        return releasedItem;
    }
}
