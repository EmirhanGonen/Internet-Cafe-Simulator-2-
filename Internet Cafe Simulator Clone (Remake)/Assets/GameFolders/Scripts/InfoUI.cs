using UnityEngine;
using TMPro;
using DG.Tweening;

public class InfoUI : MonoBehaviour
{
    public static InfoUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _infoText;

    public delegate void InfoHandle(string infoText);
    public InfoHandle InfoCallback;
    private void OnEnable()
    {
        InfoCallback += SetInfoText;
    }
    private void OnDisable()
    {
        InfoCallback -= SetInfoText;
    }
    private void Awake()
    {
        Instance = this;
    }


    private void SetInfoText(string objectName)
    {
        if (objectName != string.Empty) _infoText.SetText(objectName);
        transform.DOKill();
        transform.DOScale(objectName.Equals(string.Empty) ? 0 : 1, 0.15f).SetEase(Ease.Linear);
    }
}