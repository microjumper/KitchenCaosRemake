using UnityEngine;

public interface IContainer
{
    bool IsEmpty { get; }
    bool TryAdd(GameObject item);
    bool TryRemove(out GameObject item);
}
