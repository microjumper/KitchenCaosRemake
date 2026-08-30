using UnityEngine;

public class Counter : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject holdable;
    [SerializeField] private GameObject counterTop;

    private IItemHolder counterHolder;

    private void Awake()
    {
        counterHolder = counterTop.GetComponent<IItemHolder>();
    }

    public bool CanInteract()
    {
        return counterHolder != null && counterHolder.CanHold(holdable);
    }

    public void Interact()
    {
        if (CanInteract())
        {
            counterHolder.Hold(holdable);
        }
    }
}
