using UnityEngine;
using System.Collections;

public class PaymentState : State
{
    public Transform Payment;

    private float _amount;
    private readonly int _paymentMultiply = 5; //For Seconds

    private bool _isPaid = false;


    private const string _animationName = "Walk State";
    private const string _waitAnimationName = "Idle State";
    private Animator _animator;


    public override void OnStateEnter(params object[] parameters)
    {
        _animator = parameters[0] as Animator;

        int _animationKey = Animator.StringToHash(_animationName);

        _animator.Play(_animationKey);

        _navMeshAgent.SetDestination(Payment.position - Vector3.back);

        UseComputerState _useComputerState = parameters[1] as UseComputerState;

        _amount = _useComputerState.GetUseDuration() * _paymentMultiply;

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

        _customer.SetState(_walkState , _animator);
    }

    public float GetPayment()
    {
        _isPaid = true;
        return _amount;
    }
}