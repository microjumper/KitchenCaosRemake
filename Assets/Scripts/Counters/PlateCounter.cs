using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class PlateCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenItemDefinition plateDefinition;
    [SerializeField] private int maxPlates = 5;
    [SerializeField] private float spawnInterval = 1.5f;

    private IContainer stationContainer;

    private void Awake()
    {
        stationContainer = GetComponent<IContainer>();
    }

    private void Start()
    {
        StartCoroutine(SpawnPlates());
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.IsEmpty)
        {
            return CounterTransfer.TryTransfer(stationContainer, otherContainer);
        }

        return false;
    }

    private IEnumerator SpawnPlates()
    {
        while (true)
        {
            if (stationContainer.Count < maxPlates)
            {
                var plate = KitchenItemFactory.CreateFrom(plateDefinition);

                stationContainer.TryAdd(plate.gameObject);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
