using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTemplate : MonoBehaviour
{
    [SerializeField] private Image _itemImage, _background;

    [SerializeField] private TextMeshProUGUI _baseCostText, _infoText;

    private ItemData _tempItemData;

    public void AddToCart() => ListHolder.Instance.ShopCart.Add(_tempItemData);

    public void GetCategorize(ItemData itemData)
    {
        _tempItemData = itemData;

        _itemImage.sprite = itemData.itemSprite;
        _background.color = itemData.backgroundColor;

        _baseCostText.SetText($"${itemData.baseCost}");
        _infoText.SetText(itemData.itemName);
    }
}