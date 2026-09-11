using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuItemDefinition", menuName = "ScriptableObjects/MenuItemDefinition")]
public class MenuItemDefinition : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private List<KitchenItemDefinition> items;

    public string Name => name;
    public List<KitchenItemDefinition> Ingredients => items;
}
