using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingBox : MonoBehaviour, IInteractable, IInfo
{
    public ItemData CurrentData { set => _currentData = value; }

    public string Name => _currentData.ItemName;


    public ItemData _currentData;

    public void Interact(params object[] parameters)
    {
        GetComponent<Collider>().enabled = false;

        Instantiate(_currentData.ItemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}