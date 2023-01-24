using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    protected CustomerStateManager _customerStateManager;
    protected Customer _customer;
    protected NavMeshAgent _navMeshAgent;

    //Normal Protected olarak buraya string AnimationKey;

    private void Awake()
    {
        _customerStateManager = transform.GetComponentInParent<CustomerStateManager>();
        _customer = transform.parent.GetComponentInParent<Customer>();
        _navMeshAgent = transform.parent.GetComponentInParent<NavMeshAgent>();
    }

    public abstract void OnStateEnter(params object[] parameters);

    public abstract void OnStateUpdate(params object[] parameters);

    public abstract void OnStateExit(params object[] parameters);
}