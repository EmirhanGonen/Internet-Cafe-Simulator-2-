using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class Courier : MonoBehaviour
{
    [SerializeField] private ShippingBox _shippingBox;

    private CourierState _currentState;


    private CourierStateManager _stateManager = null;
    public CourierStateManager GetStateManager => _stateManager ? _stateManager : GetComponentInChildren<CourierStateManager>();

    private NavMeshAgent _navMeshAgent = null;
    public NavMeshAgent GetNavMeshAgent => _navMeshAgent ? _navMeshAgent : GetComponent<NavMeshAgent>();


    private Animator _animator = null;
    public Animator GetAnimator => _animator ? _animator : GetComponent<Animator>();

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _stateManager = GetComponentInChildren<CourierStateManager>();
    }


    private void Update() => _currentState?.OnStateUpdate();

    public void SetState<T>(T nextState, params object[] parameters) where T : CourierState
    {
        if (_currentState) _currentState.OnStateExit();

        _currentState = nextState;

        _currentState.OnStateEnter(parameters);
    }
    public void DoWork<T>(List<T> _datas) where T : ItemData
    {
        CourierWalkState _walkState = _stateManager.GetStates[typeof(CourierWalkState)] as CourierWalkState;

        CourierWalkState.WalkVariables _walkVariables = new() { TargetPoint = GameObject.FindGameObjectWithTag("Delivery Zone").transform.position };

        SetState(_walkState, _walkVariables, _datas);
    }
}