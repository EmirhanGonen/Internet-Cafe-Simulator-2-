using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShoppingCart : MonoBehaviour
{
    public static ShoppingCart Instance { get; private set; }

    [SerializeField] private Transform _cartItemTemplatesParent;

    [SerializeField] private TextMeshProUGUI _costText;

    private void Awake()
    {
        Instance = this;
        //_cartItemTemplate = Resources.Load<GameObject>("Prefabs/CartItemTemplate").GetComponent<CartItemTemplate>();
    }
    public void OpenCart(GameObject _panel) => _panel.SetActive(!_panel.activeSelf);

    public void SetCarts()
    {
        List<ItemData> _shoppingCart = new();

        _shoppingCart.AddRange(ListHolder.Instance.ShopCart);

        for (int i = 0; i < _cartItemTemplatesParent.childCount; i++)
            _cartItemTemplatesParent.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < _shoppingCart.Count; i++)
        {
            CartItemTemplate _template = _cartItemTemplatesParent.transform.GetChild(i).GetComponent<CartItemTemplate>();

            _template.SetTemplate(_shoppingCart[i], i + 1);

            _template.gameObject.SetActive(true);
        }
    }

    public void SetCostText() => _costText.SetText($"Buy: ${GetCartCost()}");
    public void BuyButton()
    {


        //ListHolder.Instance.ShopCart = new();
    }

    private float GetCartCost()
    {
        List<ItemData> _shoppingCart = new();

        _shoppingCart.AddRange(ListHolder.Instance.ShopCart);

        float price = 0.00f;

        foreach (ItemData item in _shoppingCart) price += item.baseCost;

        return price;
    }
}