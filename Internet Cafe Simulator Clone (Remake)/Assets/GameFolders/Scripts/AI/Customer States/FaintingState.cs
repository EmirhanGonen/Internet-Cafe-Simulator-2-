using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaintingState : State
{

    public override void OnStateEnter(params object[] parameters)
    {
        Transform hitPosition = parameters[0] as Transform;

        transform.parent.parent.LookAt(hitPosition);

        _customer.GetAnimator.enabled = false;
        _navMeshAgent.enabled = false;
    }

    public override void OnStateExit(params object[] parameters)
    {

    }

    public override void OnStateUpdate(params object[] parameters)
    {

    }
}