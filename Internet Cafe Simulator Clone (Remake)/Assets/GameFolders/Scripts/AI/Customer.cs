using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour, IInteractable
{
    private State _currentState;
    public State GetCurrentState() => _currentState;

    private CustomerStateManager _customerStateManager;

    private Animator _animator;

    private void Awake()
    {
        //_agent.areaMask |= 7; // 2 �zeri 3 = 8 8-1 7 oluyor 3.layera kadar ac�yor �s kadar layer ac�yor
        //_agent.areaMask |= 1 << 3; // 3. layeri aktif edicek
        //_agent.areaMask &= ~(2 << 3); //2. layeri pasif hale getircek

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
        if (_currentState.GetType() != typeof(PaymentState)) return;

        PaymentState _paymentState = _customerStateManager._states[typeof(PaymentState)] as PaymentState;

        //Player Inv += GetPayment
        _paymentState.GetPayment();
    }
}