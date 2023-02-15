using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CourierState : MonoBehaviour
{
    protected Courier _courier;

    private void Awake()
    {
        _courier = transform.parent.GetComponentInParent<Courier>();
    }

    public abstract void OnStateEnter(params object[] parameters);

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
}