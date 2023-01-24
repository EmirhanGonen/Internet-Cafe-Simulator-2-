using UnityEngine;

public class UseComputerState : State
{
    private Animator _animator;
    private const string _animationName = "UseComputer State";

    private DeskManager _desk;

    private Transform _monitor;
    private Transform _chair;

    private Collider _collider;

    private float _timer;
    private int _useDuration;
    public int GetUseDuration() => _useDuration;

    private void Start()
    {
        _collider = _customer.GetComponent<Collider>();
    }

    public override void OnStateEnter(params object[] parameters)
    {
        _animator = parameters[0] as Animator;

        _desk = parameters[1] as DeskManager;

        _monitor = (parameters[1] as DeskManager).GetDesk()._computerParts[PartType.Monitor].transform;
        _chair = (parameters[1] as DeskManager).GetDesk()._computerParts[PartType.Chair].transform;

        _desk.SetMonitorScreen();

        _collider.enabled = false;
        _customer.transform.SetParent(_chair);
        Invoke(nameof(SetLocalPositionByDelay), 1.35f);

        int _animationKey = Animator.StringToHash(_animationName);

        _animator.Play(_animationKey);

        DecisionToUseDuration();
    }

    public override void OnStateExit(params object[] parameters)
    {
        _customer.transform.transform.SetParent(null);
        _collider.enabled = true;
        _navMeshAgent.enabled = true;
    }

    public override void OnStateUpdate(params object[] parameters)
    {
        _timer += Time.deltaTime;

        //Using Computer

        if (_timer < _useDuration) return;

        SitUpComputer();
    }

    private void SitUpComputer()
    {
        //Payment State

        PaymentState _paymentState = _customerStateManager._states[typeof(PaymentState)] as PaymentState;

        _customer.SetState(_paymentState , _animator , this);
    }

    private void DecisionToUseDuration() => _useDuration = GetRandomUseDuration();

    private int GetRandomUseDuration() => Random.Range(10, 20); //Decide Use Duration
    
    private void SetLocalPositionByDelay()
    {
        _customer.transform.localPosition = Vector3.zero; //Sandalyeye oturmasý için
        _customer.transform.localRotation = Quaternion.Euler(0, 0, 0); // Ekrana bakmasý için
    }
}