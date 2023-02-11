using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartItemTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName, _itemPrice;
    [SerializeField] private Image _itemImage;

    public void SetTemplate(ItemData _data)
    {
        _itemName.SetText(_data.itemName);
        _itemPrice.SetText($"${_data.baseCost}");

        _itemImage.sprite = _data.itemSprite;
    }
}