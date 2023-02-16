using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FaintingState : State
{

    public override void OnStateEnter(params object[] parameters)
    {
        Transform hitPosition = parameters[0] as Transform;

        transform.parent.parent.LookAt(hitPosition);

        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        _customer.GetAnimator.enabled = false;
        _navMeshAgent.enabled = false;
        _customer.GetComponent<Collider>().enabled = false;

        if (_customer.CustomerIsCrime())
            PlayerWallet.Instance.Money += _customer.PaymentAmount;

        StartCoroutine(nameof(Respawn));
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(5);

        transform.parent.parent.GetChild(0).gameObject.SetActive(false);
        transform.parent.parent.GetChild(1).gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(10);

        _navMeshAgent.enabled = true;
        _customer.GetAnimator.enabled = true;
        _customer.GetComponent<Collider>().enabled = true;


        transform.parent.parent.GetChild(0).gameObject.SetActive(true);
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);


        IdleState _idleState = _customerStateManager._states[typeof(IdleState)] as IdleState;

        _customer.SetState(_idleState, _customer.GetAnimator);
    }


    public override void OnStateExit(params object[] parameters)
    {

    }
    public override void OnStateUpdate(params object[] parameters)
    {

    }
}