using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; } = null;

    [SerializeField] private GameObject _playerWalletMenu;
    [SerializeField] private GameObject _playerWalletInfoUI;
    [SerializeField] private KeyCode _menuInteractionKeyCode = KeyCode.Tab;
    private bool _isInteractableToMenu = true;

    [SerializeField] private TextMeshProUGUI _moneyText;

    private delegate void WalletMenuHandler();
    private WalletMenuHandler _walletMenuCallback;

    [ContextMenu("GetMoney")]
    public void GetMoney() => Money += 50;
    [ContextMenu("LoseMoney")]

    public void LoseMoney() => Money -= 50;


    public float Money
    {
        get => _money; set
        {
            float _tempValue = value - _money;

            _money = value;

            TextMeshProUGUI _text = _playerWalletInfoUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

            _text.color = _tempValue < 0 ? Color.red : Color.green;

            string sign = _tempValue < 0 ? "-" : "+";

            _text.SetText($"${ " " + sign + " " + Mathf.Abs(_tempValue) } ");

            _playerWalletInfoUI.transform.DOKill();

            _playerWalletInfoUI.transform.DOLocalMoveX(793, 0.70f).
                OnComplete(() => _playerWalletInfoUI.transform.DOLocalMoveX(1300, 0.70f).SetDelay(0.50f));
        }
    }
    private float _money = 200.00f;


    private Character_Controller _controller;

    #region Unity Methods

    private void OnEnable()
    {
        _walletMenuCallback += () => SetInteractable(false);
        _walletMenuCallback += DoKillMenu;
        _walletMenuCallback += InteractMenu;
        _walletMenuCallback += SetMoneyText;
        _walletMenuCallback += SetCharacterMoveable;
    }
    private void OnDisable()
    {
        _walletMenuCallback -= () => SetInteractable(false);
        _walletMenuCallback -= DoKillMenu;
        _walletMenuCallback -= InteractMenu;
        _walletMenuCallback -= SetMoneyText;
        _walletMenuCallback -= SetCharacterMoveable;
    }


    private void Awake()
    {
        Instance = this;
        _controller = GetComponent<Character_Controller>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_menuInteractionKeyCode) | !_isInteractableToMenu) return;
        _walletMenuCallback?.Invoke();
    }

    #endregion

    #region WalletMenuCallbacks
    private void DoKillMenu() => _playerWalletMenu.transform.DOKill();
    private void InteractMenu() => _playerWalletMenu.transform.DOLocalMoveX(_playerWalletMenu.transform.localPosition.x == 0 ? 2000 : 0, 0.70f).SetEase(Ease.InOutBack).OnComplete(() => { SetInteractable(true); });
    private void SetMoneyText() => _moneyText.SetText($"${_money}");
    private void SetCharacterMoveable()
    {
        if (!_controller.GetCanWalk)
        {
            _controller.CanControlCallback();
            return;
        }
        _controller.CantControlCallback();
    }

    private void SetInteractable(bool value) => _isInteractableToMenu = value;
    #endregion
}