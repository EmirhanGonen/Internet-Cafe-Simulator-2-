using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CargoCompany : MonoBehaviour
{
    public static CargoCompany Instance { get; private set; } = null;

    [SerializeField] private Courier _courier;

    private void Awake() => Instance = this;


    public async void GetWork(List<ItemData> datas)
    {
       await Task.Delay(Random.Range(5, 10) * 1000);

        _courier.gameObject.SetActive(true);

        _courier.DoWork(datas);
    }
}