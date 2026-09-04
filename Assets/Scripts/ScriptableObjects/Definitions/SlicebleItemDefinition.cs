using UnityEngine;

[CreateAssetMenu(fileName = "SlicebleDefinition", menuName = "ScriptableObjects/SlicebleDefinition")]
public class SlicebleItemDefinition : ScriptableObject
{
    [SerializeField] private KitchenItemDefinition input;
    [SerializeField] private KitchenItemDefinition output;
    [SerializeField] private int cutsRequired;

    public KitchenItemDefinition Input => input;
    public KitchenItemDefinition Output => output;
    public int CutsRequired => cutsRequired;
}