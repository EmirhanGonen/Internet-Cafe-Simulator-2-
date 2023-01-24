using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour , IInteractable
{
    private State _currentState;
    public State GetCurrentState() => _currentState;

    private CustomerStateManager _customerStateManager;

    private Animator _animator;

    private void Awake()
    {
        _customerStateManager = GetComponentInChildren<CustomerStateManager>();

        _animator = GetComponent<Animator>();

        IdleState idleState = _customerStateManager._states[typeof(IdleState)] as IdleState;

        _currentState = idleState;

        SetState(idleState, _animator);
    }

    public void SetState(State nextState, params object[] parameters)
    {
        _currentState.OnStateExit(parameters);

        _currentState = nextState;

        _currentState.OnStateEnter(parameters);
    }

    private void Update()
    {
        if (_currentState) _currentState.OnStateUpdate();
    }

    
    public void Interact(params object[] parameters)
    {
        Debug.Log(_currentState.GetType() != typeof(PaymentState));

        if (_currentState.GetType() != typeof(PaymentState)) return;

        PaymentState _paymentState  = _customerStateManager._states[typeof(PaymentState)] as PaymentState;

        //Player Inv += GetPayment
        _paymentState.GetPayment();
    }
}