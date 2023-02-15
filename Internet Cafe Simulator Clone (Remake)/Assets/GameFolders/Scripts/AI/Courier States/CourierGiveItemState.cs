using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierGiveItemState : CourierState
{
    [SerializeField] private ShippingBox _shippingBox;

    private List<ItemData> _items;

    private const string ANIMATIN_KEY = "Idle State";

    private Transform _leftTopCorner, _rightDownCorner;

    public override void OnStateEnter(params object[] parameters)
    {
        _items = parameters[0] as List<ItemData>;

        int animationHash = Animator.StringToHash(ANIMATIN_KEY);

        _courier.GetAnimator.Play(animationHash);

        Transform _deliveryZone = GameObject.FindGameObjectWithTag("Delivery Zone").transform;

        _leftTopCorner = _deliveryZone.GetChild(0).transform;
        _rightDownCorner = _deliveryZone.GetChild(1).transform;


        for (int i = 0; i < _items.Count; i++)
        {
            ShippingBox _tempShippingBox = Instantiate(_shippingBox, GetRandomSpawnPosition(), Quaternion.identity);
            _tempShippingBox.CurrentData = _items[i];
        }
    }

    public override void OnStateUpdate()
    {

    }
    public override void OnStateExit()
    {

    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(_rightDownCorner.position.x, _leftTopCorner.position.x);

        float randomZ = Random.Range(_rightDownCorner.position.z, _leftTopCorner.position.z);

        return new(randomX, -0.30f, randomZ);
    }
}