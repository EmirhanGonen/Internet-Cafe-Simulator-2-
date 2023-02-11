using UnityEngine;
using System.Collections.Generic;

public class ListHolder : MonoBehaviour
{
    #region Singleton
    public static ListHolder Instance { get; private set; }
    #endregion

    #region Lists

    #region DeskManager

    public List<DeskManager> AvailableDesks = new();
    public int AvailableDesksCount => AvailableDesks.Count;

    #endregion

    #region Item Template

    public List<ItemTemplate> itemTemplate;

    #endregion

    #region CartList

    public List<ItemData> ShopCart;
    public int ShopCartCount => ShopCart.Count;

    #endregion

    #endregion

    private void Awake()
    {
        Instance = this;
    }
}