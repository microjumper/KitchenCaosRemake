using UnityEngine;

[CreateAssetMenu(fileName = "New Definition", menuName = "ScriptableObjects/KitchenItemDefinition")]
public class KitchenItemDefinition : ScriptableObject
{
    [SerializeField] private GameObject visual;
    [SerializeField] private Sprite sprite;

    public GameObject Visual => visual;
    public Sprite Sprite => sprite;
}
