using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class StoveStation : MonoBehaviour, IInteractable
{
    private IContainer stationContainer;

    private void Awake()
    {
        stationContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.IsEmpty)
        {
            return StationTransferer.TryTransfer(stationContainer, otherContainer);
        }

        if (stationContainer.IsEmpty)
        {
            return StationTransferer.TryTransfer(otherContainer, stationContainer);
        }

        return false;
    }
}
