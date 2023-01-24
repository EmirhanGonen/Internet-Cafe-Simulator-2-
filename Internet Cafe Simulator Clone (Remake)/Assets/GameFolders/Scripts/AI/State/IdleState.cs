using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class IdleState : State
{
    private Animator _animator;
    private const string _animationKey = "Idle State";

    private float _decisionDelay;
    private float _timer;

    public override void OnStateEnter(params object[] parameters)
    {
        _animator = parameters[0] as Animator;

        int idleAnimHashCode = Animator.StringToHash(_animationKey);

        _animator.Play(idleAnimHashCode);

        SetDelay();
    }

    public override void OnStateExit(params object[] parameters)
    {
        _decisionDelay = 0f;
        _timer = 0f;

        //_animator = null;
    }

    public override void OnStateUpdate(params object[] parameters)
    {
        _timer += Time.deltaTime;

        if (_decisionDelay > _timer) return;

        MakeDecision();

        // Delayla MakeDecision
    }


    public void MakeDecision(params object[] parametres)
    {

        //Decision diye state ac�p t�m decisionlar� orda yapabilrm ��nk� decision ayr� bi state

        //Bak�cak random gitsemmi gitmesemmi diye d���ncek e�er gitmeye karar verirse bak�cak available
        //masa varm� yada masaya ge�ebilecekmiyim (Masa bo� olabilir ama AvailableTime ge�memi� olur)
        //e�er yoksa geri �ehirde gezicek varsa masaya geliyorum diye haber vericek

        bool isDecideGoCafe = Random.Range(0, 2) == 1;

        if (isDecideGoCafe & CanGoCafe())
        {

            GoDesk();
            return;
        }

        Debug.Log("Masaya gitmemeye karar verdi");


        //Else not decide go cafe

        WalkState walkState = _customerStateManager._states[typeof(WalkState)] as WalkState;

        _customer.SetState(walkState, _animator, new System.Action(() => { _customer.SetState(this, _animator); }));
    }

    [Button("Go Desk")]
    private void GoDesk()
    {
        WalkToDeskState walkDeskState = _customerStateManager._states[typeof(WalkToDeskState)] as WalkToDeskState;

        Debug.Log("Masaya gitmeye karar verdi");

        //Go Cafe

        DeskManager selectedDesk = ListHolder.Instance.AvailableDesks[Random.Range(0, ListHolder.Instance.AvailableDesksCount)]; //gidice�i masay� random seciyor

        _customer.SetState(walkDeskState, selectedDesk, _animator);

        selectedDesk.SetNotAvailable();

        //selectedDesk.UseByCustomer(); //Haber veriyor geliyorum diye;
    }

    private void SetDelay() => _decisionDelay = Random.Range(5, 8);

    private bool CanGoCafe()
    {
        ListHolder listHolder = ListHolder.Instance;

        return listHolder.AvailableDesksCount > 0;
    }
}