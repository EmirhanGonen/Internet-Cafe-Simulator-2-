using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour, IInteractable, IDamagable
{
    private State _currentState;
    private State _crimeState = null;
    public State GetCurrentState() => _currentState;

    private CustomerStateManager _customerStateManager;

    private Animator _animator;

    public Animator GetAnimator => _animator;

    private float _paymentAmount;
    public float PaymentAmount { get => _paymentAmount; set => _paymentAmount = value; }

    private NavMeshAgent _agent;

    private void Awake()
    {
        //_agent.areaMask |= 7; // 2 uzeri 3 = 8 8-1 7 oluyor 3.layera kadar aciyor us kadar layer aciyor
        //_agent.areaMask |= 1 << 3; // 3. layeri aktif edicek
        //_agent.areaMask &= ~(2 << 3); //2. layeri pasif hale getircek

        _customerStateManager = GetComponentInChildren<CustomerStateManager>();

        _animator = GetComponent<Animator>();

        _agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        SetIdleState();
    }
    private void SetIdleState()
    {

        IdleState idleState = _customerStateManager._states[typeof(IdleState)] as IdleState;

        SetState(idleState, _animator);
    }

    public void SetState(State nextState, params object[] parameters)
    {
        _currentState?.OnStateExit(parameters);

        _currentState = nextState;

        _currentState?.OnStateEnter(parameters);
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (!other.CompareTag("Cafe Door")) return;

        WalkState _walkState = _customerStateManager._states[typeof(WalkState)] as WalkState;

        //E??er cafe kap??s??na temas ettiyse yani ya kafeye giriyordur yada ????k??yordur
        // cafeden ????kmas?? i??in statesinin walkstate olmas?? laz??m onu kontrol edip aremaskini kapat??yorum

        if (_currentState == _walkState) 
            _agent.areaMask &= ~(3 << 3);
    }
    public void Interact(params object[] parameters)
    {
        if (_currentState.GetType() != typeof(PaymentState)) return;

        PaymentState _paymentState = _customerStateManager._states[typeof(PaymentState)] as PaymentState;

        //Player Inv += GetPayment
        PlayerWallet.Instance.Money += _paymentState.GetPayment();
    }
    public void TakeDamage(Transform damagePosition)
    {
        UseComputerState _useComputerState = _customerStateManager._states[typeof(UseComputerState)] as UseComputerState;

        //e??er sopayla hasar ald??????nda masada ise buray?? ??al????t??rcak
        //buda masa bo??als??n ve ba??ka m????teriler gelebilsin vs diye

        if (_currentState == _useComputerState)
            _useComputerState.SitUpComputer(isFainted: true);

        FaintingState _faintingState = _customerStateManager._states[typeof(FaintingState)] as FaintingState;

        SetState(_faintingState, damagePosition);
    }

    public bool CustomerIsCrime() => _crimeState != null;
}