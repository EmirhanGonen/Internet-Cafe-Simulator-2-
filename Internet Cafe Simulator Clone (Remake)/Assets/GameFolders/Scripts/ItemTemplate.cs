using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ItemTemplate : MonoBehaviour
{
    [SerializeField] private Image _itemImage, _background;

    [SerializeField] private TextMeshProUGUI _baseCostText, _infoText;

    private ItemData _tempItemData;

    private Vector3 _itemImageFirstScale = Vector3.one;

    private void Start()
    {
        _itemImageFirstScale = _itemImage.transform.localScale;
    }

    public void AddToCart() => ListHolder.Instance.ShopCart.Add(_tempItemData);

    public void GetCategorize(ItemData itemData)
    {
        _tempItemData = itemData;

        _itemImage.sprite = itemData.ItemSprite;
        _background.color = itemData.BackgroundColor;

        _baseCostText.SetText($"${itemData.BaseCost}");
        _infoText.SetText(itemData.ItemName);
    }

    private void OnMouseEnter() => DoScale(_itemImage.transform , Vector3.one * 1.25f, 0.15f);
    private void OnMouseExit() => DoScale(_itemImage.transform, _itemImageFirstScale, 0.15f);

    private void DoScale(Transform _item, Vector3 _endValue, float _timeDuration) => _item.DOScale(_endValue, _timeDuration).SetEase(Ease.Linear);
}