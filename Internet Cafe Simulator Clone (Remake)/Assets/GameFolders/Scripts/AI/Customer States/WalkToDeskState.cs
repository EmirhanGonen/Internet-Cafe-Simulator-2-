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
        _agent.areaMask |= 1 << 3;

        _desk = parameters[0] as DeskManager;

        _desk.InfoForCustomersCallback += CheckDesk;

        _targetTransform = _desk.GetDesk()._computerParts[PartType.Chair].transform;

        _animator = parameters[1] as Animator;

        int animationHash = Animator.StringToHash(_animationName);

        _animator.Play(animationHash);
    }

    public override void OnStateExit(params object[] parameters)
    {
        //_customer.transform.LookAt(_desk.GetDesk()._computerParts[PartType.Monitor].transform);
        _desk.InfoForCustomersCallback -= CheckDesk;
        _agent.enabled = false;
    }

    public override void OnStateUpdate(params object[] parameters)
    {
        _agent.SetDestination(_targetTransform.position);

        if (Vector3.Distance(_customer.transform.position, _targetTransform.position) < 0.30f)
        {
            UseComputerState _useComputerState = _customerStateManager._states[typeof(UseComputerState)] as UseComputerState;
            // _desk.UsedByCustomer();
            _customer.SetState(_useComputerState, _animator, _desk);
        }
    }
    public void CheckDesk()
    {
        bool _deskIsCompleted = _desk.GetDesk().IsCompleted();

        if (_deskIsCompleted) return;

        WalkState _walkState = _customerStateManager._states[typeof(WalkState)] as WalkState;

        _customer.SetState(_walkState, _animator);
    }
}