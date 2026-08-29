using UnityEngine;

public class TargetFacade : MonoBehaviour
{
    public IInteractable Interactable { get; private set; }
    public ISelectable Selectable { get; private set; }

    private void Awake()
    {
        Interactable = GetComponent<IInteractable>();
        Selectable = GetComponent<ISelectable>();
    }
}
