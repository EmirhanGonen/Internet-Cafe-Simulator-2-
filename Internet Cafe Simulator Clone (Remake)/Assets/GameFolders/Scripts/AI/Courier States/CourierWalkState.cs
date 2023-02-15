using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierWalkState : CourierState
{
    public class WalkVariables
    {
        public Vector3 TargetPoint;
    }

    private Vector3 _targetPoint;
    private List<ItemData> _items;


    private const string ANIMATION_KEY = "Walk State";


    public override void OnStateEnter(params object[] parameters)
    {
        _targetPoint = (parameters[0] as WalkVariables).TargetPoint;

        _items = parameters[1] as List<ItemData>;

        int animationHash = Animator.StringToHash(ANIMATION_KEY);

        _courier.GetAnimator.Play(animationHash);

        _courier.GetNavMeshAgent.SetDestination(_targetPoint);
    }

    public override void OnStateUpdate()
    {
        if (!IsReach()) return;

        CourierGiveItemState _giveItemState = _courier.GetStateManager.GetStates[typeof(CourierGiveItemState)] as CourierGiveItemState;

        _courier.SetState(_giveItemState, _items);
    }

    public override void OnStateExit() => _items = null;
    private bool IsReach() => _courier.GetNavMeshAgent.remainingDistance < 0.50f;
}