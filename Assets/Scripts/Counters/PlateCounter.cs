using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class PlateCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenItemDefinition plateDefinition;
    [SerializeField] private int maxPlates = 5;
    [SerializeField] private float spawnInterval = 1.5f;

    private IContainer counterContainer;

    private int numberOfPlates = 0;

    private void Awake()
    {
        counterContainer = GetComponent<IContainer>();
    }

    private void Start()
    {
        StartCoroutine(SpawnPlates());
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.HasItem)
        {
            return false;
        }

        var transferred = ItemTransfer.TryTransfer(counterContainer, otherContainer);

        if (transferred)
        {
            numberOfPlates--;
        }

        return transferred;
    }

    private IEnumerator SpawnPlates()
    {
        while (true)
        {
            if (numberOfPlates < maxPlates)
            {
                var plate = KitchenItemFactory<Plate>.CreateFrom(plateDefinition);

                counterContainer.TryStore(plate);

                numberOfPlates++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
