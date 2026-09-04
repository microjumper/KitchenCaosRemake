using UnityEngine;

[CreateAssetMenu(fileName = "CookableItemDefinition", menuName = "ScriptableObjects/CookableItemDefinition")]
public class CookableItemDefinition : ScriptableObject
{
    [SerializeField] private KitchenItemDefinition raw;
    [SerializeField] private KitchenItemDefinition cooked;
    [SerializeField] protected KitchenItemDefinition burned;
    [SerializeField] private float cookingTime;
    [SerializeField] private float overtime;

    public KitchenItemDefinition Raw => raw;
    public KitchenItemDefinition Cooked => cooked;
    public KitchenItemDefinition Burned => burned;
    public float CookingTime => cookingTime;
    public float Overtime => overtime;
}
