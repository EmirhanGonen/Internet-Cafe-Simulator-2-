using UnityEngine;
using UnityEngine.AI;

public class WalkToDeskState : State
{
    private DeskManager _desk;
    private Transform _targetTransform;

    private NavMeshAgent _agent;

    private Animator _animator;
    private const string _animationName = "Walk State";

    private void Start()
    {
        _agent = _customer.GetComponent<NavMeshAgent>();
    }

    public override void OnStateEnter(params object[] parameters)
    {
        _desk = parameters[0] as DeskManager;

        _targetTransform = _desk.GetDesk()._computerParts[PartType.Chair].transform;

        _animator = parameters[1] as Animator;

        int animationHash = Animator.StringToHash(_animationName);

        _animator.Play(animationHash);
    }

    public override void OnStateExit(params object[] parameters)
    {
        //_customer.transform.LookAt(_desk.GetDesk()._computerParts[PartType.Monitor].transform);
        _agent.enabled = false;
    }

    public override void OnStateUpdate(params object[] parameters)
    {
        _agent.SetDestination(_targetTransform.position);

        if (Vector3.Distance(_customer.transform.position, _targetTransform.position) < 0.20f)
        {
            UseComputerState _useComputerState = _customerStateManager._states[typeof(UseComputerState)] as UseComputerState;
           // _desk.UsedByCustomer();
            _customer.SetState(_useComputerState , _animator , _desk);
        }
    }
}