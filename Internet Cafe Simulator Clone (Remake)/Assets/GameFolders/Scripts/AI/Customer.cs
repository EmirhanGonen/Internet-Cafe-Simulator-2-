using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour, IInteractable , IDamagable
{
    private State _currentState;
    private State _crimeState = null;
    public State GetCurrentState() => _currentState;

    private CustomerStateManager _customerStateManager;

    private Animator _animator;

    public Animator GetAnimator => _animator;

    private float _paymentAmount;
    public float PaymentAmount { get => _paymentAmount; set => _paymentAmount = value; }

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

    public void SetCrimeState(CrimeState crimeState, params object[] parameters)
    {
        _crimeState?.OnStateExit();

        _crimeState = crimeState;

        _crimeState?.OnStateEnter(parameters);
    }


    private void Update()
    {
        if (_currentState) _currentState.OnStateUpdate();
        _crimeState?.OnStateUpdate();
    }
    public void Interact(params object[] parameters)
    {
        if (_currentState.GetType() != typeof(PaymentState)) return;

        PaymentState _paymentState = _customerStateManager._states[typeof(PaymentState)] as PaymentState;

        //Player Inv += GetPayment
        _paymentState.GetPayment();
    }
    public void TakeDamage(Transform damagePosition)
    {
        FaintingState _faintingState = _customerStateManager._states[typeof(FaintingState)] as FaintingState;

        SetState(_faintingState , damagePosition);
    }

    public bool CustomerIsCrime() =>_crimeState != null;
}