using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class ItemData : ScriptableObject
{
    public Item ItemPrefab;

    public string ItemName;

    public Sprite ItemSprite;

    public Color BackgroundColor;

    public int BaseCost;
}