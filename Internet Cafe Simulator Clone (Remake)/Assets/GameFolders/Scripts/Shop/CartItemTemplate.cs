using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartItemTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName, _itemPrice , _indexText;
    [SerializeField] private Image _itemImage , _itemImageBackground;
    [SerializeField] private Button _removeButton;


    private ItemData _itemData = null;

    private void Start()
    {
        _removeButton.onClick.AddListener(()=> { ShoppingCart.Instance.SetCostText(); });
    }

    public void SetTemplate(ItemData _data , int Index)
    {
        _itemData = _data;

        _itemName.SetText(_data.ItemName);
        _itemPrice.SetText($"${_data.BaseCost}");
        _indexText.SetText(Index.ToString());

        _itemImage.sprite = _data.ItemSprite;
        _itemImageBackground.color = _data.BackgroundColor;
    }

    public void RemoveFromShoppingCart() 
    {
        transform.gameObject.SetActive(false);

        ListHolder.Instance.ShopCart.Remove(_itemData);
        _itemData = null;

        ShoppingCart.Instance.SetCarts();
        ShoppingCart.Instance.SetCostText();
    }
}