using UnityEngine;
using System.Collections.Generic;

public class ShoppingCart : MonoBehaviour
{
    private CartItemTemplate _cartItemTemplate;

    private void Awake()
    {
        _cartItemTemplate = Resources.Load<GameObject>("Prefabs/CartItemTemplate").GetComponent<CartItemTemplate>();
    }
    public void OpenCart(GameObject _panel)
    {
        List<ItemData> _shoppingCart = new();

        _shoppingCart.AddRange(ListHolder.Instance.ShopCart);

        _panel.SetActive(true);
    }

    public void BuyButton()
    {
        List<ItemData> _shoppingCart = new();

        _shoppingCart.AddRange(ListHolder.Instance.ShopCart);

        float price = 0.00f;

        foreach (ItemData item in _shoppingCart) price += item.baseCost;
        
        //ListHolder.Instance.ShopCart = new();
    }
}