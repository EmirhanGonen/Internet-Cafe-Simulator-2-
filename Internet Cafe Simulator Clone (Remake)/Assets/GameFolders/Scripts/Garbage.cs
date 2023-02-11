using UnityEngine;
using DG.Tweening;

public class Garbage : MonoBehaviour, IInfo
{
    [SerializeField] private string _name;

    public string Name => _name;

    //Cafe Manager tarz� bi kod act�g�m zaman i�inde s�cakl�k kirlilik tutucam ve onenable da kirlilik++ ondisablede kirlilik--; olcak
    public void GetCleaned() => transform.DOScale(0, .70f).SetEase(Ease.OutBounce).SetDelay(0.20f);
}