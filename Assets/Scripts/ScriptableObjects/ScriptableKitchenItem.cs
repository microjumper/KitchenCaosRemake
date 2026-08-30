using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchen Item", menuName = "ScriptableObjects/Kitchen/Item")]
public class ScriptableKitchenItem : ScriptableObject
{
    [SerializeField] private GameObject itemVisual;
    [SerializeField] private Sprite itemSprite;
}
