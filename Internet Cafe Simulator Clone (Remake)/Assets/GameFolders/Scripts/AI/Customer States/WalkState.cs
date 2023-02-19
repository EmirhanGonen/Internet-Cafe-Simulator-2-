using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WalkState : State
{
    public UnityEvent OnReach = new();

    private NavMeshAgent _agent;

    private Animator _animator;
    private const string _animationKey = "Walk State";

    private bool isGetRandomPosition = false;

    private void Start()
    {
        _agent = _customer.GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter(params object[] parameters)
    {
        if (!_agent.enabled) _agent.enabled = true;

        OnReach.RemoveAllListeners();

        _animator = parameters[0] as Animator;

        int animationHash = Animator.StringToHash(_animationKey);

        _animator.Play(animationHash);

        _agent.SetDestination(GetRandomPoint(_customer.transform.position, 25));
    }

    public override void OnStateExit(params object[] parameters)
    {
        isGetRandomPosition = false;
    }

    public override void OnStateUpdate(params object[] parameters)
    {
        if (!IsReach() | !isGetRandomPosition) return;

        _customer.SetState(_customerStateManager._states[typeof(IdleState)] as IdleState, _animator);
    }

    private bool IsReach() => Vector3.Distance(_customer.transform.position, _agent.destination) < 0.50f;

    public Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        Transform _cafe = GameObject.FindGameObjectWithTag("Cafe Ground").transform;

        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMesh.SamplePosition(randomPos, out NavMeshHit hit, maxDistance, NavMesh.AllAreas);

        if (Vector3.Distance(hit.position, _cafe.position) < 15f)
            return GetRandomPoint(center, maxDistance);

        isGetRandomPosition = true;

        return hit.position;
    }
}