using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public Sprite itemSprite;

    public Color backgroundColor;

    public int baseCost;
}