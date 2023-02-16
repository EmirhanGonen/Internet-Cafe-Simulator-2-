using UnityEngine;
using System.Collections;

public class PaymentState : State
{
    private Transform _paymentPosition;

    private float _amount;
    private readonly int _paymentMultiply = 5; //For Seconds

    private bool _isPaid = false;


    private const string _animationName = "Walk State";
    private const string _waitAnimationName = "Idle State";
    private Animator _animator;


    public override void OnStateEnter(params object[] parameters)
    {
        //yüzdelik ihtimal ile ya ödesin ya kaçsýn
        _animator = parameters[0] as Animator;

        UseComputerState _useComputerState = parameters[1] as UseComputerState;

        _amount = _useComputerState.GetUseDuration() * _paymentMultiply;

        _customer.PaymentAmount = _amount;

        bool isPaying = Random.Range(0, 2) == 0;

        if (!isPaying)
        {
            CrimeState _crimeState = _customerStateManager._states[typeof(CrimeState)] as CrimeState;
            CrimeState.CrimeStateVariables _crimeStateVariables = new() { PaymentAmount = _amount };

            _customer.SetCrimeState(_crimeState, _crimeStateVariables);

            WalkState _walkState = _customerStateManager._states[typeof(WalkState)] as WalkState;

            _customer.SetState(_walkState, _animator);

            return;
        }


        int _animationKey = Animator.StringToHash(_animationName);

        _animator.Play(_animationKey);

        _paymentPosition = GameObject.FindGameObjectWithTag("MyComputer").transform;

        _navMeshAgent.SetDestination(_paymentPosition.position - Vector3.back);


        StartCoroutine(nameof(CO_CheckPayment));
    }

    public override void OnStateExit(params object[] parameters)
    {

    }
    public override void OnStateUpdate(params object[] parameters)
    {
        if (_navMeshAgent.remainingDistance > 0.2f) return;

        int _animationKey = Animator.StringToHash(_waitAnimationName);
        _animator.Play(_animationKey);
    }

    private IEnumerator CO_CheckPayment()
    {
        while (!_isPaid) yield return null;

        WalkState _walkState = _customerStateManager._states[typeof(WalkState)] as WalkState;

        _customer.SetState(_walkState, _animator);
    }

    public float GetPayment()
    {
        _isPaid = true;
        return _amount;
    }
}