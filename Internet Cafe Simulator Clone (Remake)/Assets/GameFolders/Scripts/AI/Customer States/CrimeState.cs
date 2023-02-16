using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrimeState : State
{
    public class CrimeStateVariables
    {
        public float PaymentAmount;
    }

    [SerializeField] private Canvas _canvas;

    [SerializeField] private Image _fillImage;
    [SerializeField] private TextMeshProUGUI _remainingSecondText;

    private float _paymentAmount = 0.00f;
    private float _maxSecondsToGetMoney = 30.00f;

    private float _tempMaxSecondsToGetMoney = 0.00f;

    public override void OnStateEnter(params object[] parameters)
    {
        _paymentAmount = (parameters[0] as CrimeStateVariables).PaymentAmount;
        _tempMaxSecondsToGetMoney = _maxSecondsToGetMoney;

        _canvas.gameObject.SetActive(true);
    }

    public override void OnStateExit(params object[] parameters)
    {
        _canvas.gameObject.SetActive(false);
    }

    public override void OnStateUpdate(params object[] parameters)
    {
        if (_tempMaxSecondsToGetMoney <= 0)
            _customer.SetCrimeState(null);

        _tempMaxSecondsToGetMoney -= Time.deltaTime;

        _remainingSecondText.SetText($"{_tempMaxSecondsToGetMoney: 0}");

        _fillImage.fillAmount = (_maxSecondsToGetMoney - _tempMaxSecondsToGetMoney) / _maxSecondsToGetMoney;
    }
}