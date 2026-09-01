using UnityEngine;

public interface IContainer
{
    bool IsEmpty { get; }
    GameObject Peek();
    bool TryAdd(GameObject item);
    bool TryRemove(out GameObject item);
}
