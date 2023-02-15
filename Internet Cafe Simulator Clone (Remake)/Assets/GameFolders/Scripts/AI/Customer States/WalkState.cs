using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WalkState : State
{
    public UnityEvent OnReach = new();

    private NavMeshAgent _agent;
    private bool _canGetRandomPosition = true;

    private Animator _animator;
    private const string _animationKey = "Walk State";

    private void Start()
    {
        _agent = _customer.GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter(params object[] parameters)
    {
        if (!_agent.enabled) _agent.enabled = true;

        OnReach.RemoveAllListeners();

        _animator = parameters[0] as Animator;

        int idleAnimHashCode = Animator.StringToHash(_animationKey);

        _animator.Play(idleAnimHashCode);

        //OnReach.AddListener(parameters[1] as UnityAction);
    }

    public override void OnStateExit(params object[] parameters)
    {
        // _animator = null;
        _canGetRandomPosition = true;
    }

    public override void OnStateUpdate(params object[] parameters)
    {
        _agent.SetDestination(GetRandomPoint(_customer.transform.position, 25));
        //_agent.SetDestination(Vector3.forward * 1);

        if (!IsReach()) return;

        _customer.SetState(_customerStateManager._states[typeof(IdleState)] as IdleState, _animator);

        //OnReach?.Invoke();
    }

    private bool IsReach() => Vector3.Distance(_customer.transform.position, _agent.destination) < 0.50f;

    public Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        if (!_canGetRandomPosition) return _agent.destination;

        _canGetRandomPosition = false;

        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMesh.SamplePosition(randomPos, out NavMeshHit hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }
}