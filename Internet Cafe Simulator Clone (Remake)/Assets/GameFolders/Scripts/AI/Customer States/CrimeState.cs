using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimeState : State
{
    public class CrimeStateVariables
    {
        public float PaymentAmount;
    }

    private float _paymentAmount = 0.00f;

    public override void OnStateEnter(params object[] parameters)
    {
        _paymentAmount = (parameters[0] as CrimeStateVariables).PaymentAmount;
    }

    public override void OnStateExit(params object[] parameters)
    {

    }

    public override void OnStateUpdate(params object[] parameters)
    {
        Debug.Log("Crime");

    }
}