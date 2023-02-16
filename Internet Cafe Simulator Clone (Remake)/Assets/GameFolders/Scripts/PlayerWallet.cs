using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; } = null;



    [SerializeField] private GameObject _playerWalletMenu;
    [SerializeField] private KeyCode _menuInteractionKeyCode = KeyCode.Tab;
    private bool _isInteractableToMenu = true;

    [SerializeField] private TextMeshProUGUI _moneyText;

    private delegate void WalletMenuHandler();
    private WalletMenuHandler _walletMenuCallback;

    public float Money { get => _money; set => _money = value; }
    private float _money = 200.00f;


    private Character_Controller _controller;

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


    #region WalletMenuCallbacks
    private void DoKillMenu() => _playerWalletMenu.transform.DOKill();
    private void InteractMenu() => _playerWalletMenu.transform.DOLocalMoveX(_playerWalletMenu.transform.localPosition.x == 0 ? 2000 : 0, 0.70f).SetEase(Ease.InOutBack).OnComplete(() => { SetInteractable(true); });
    private void SetMoneyText() => _moneyText.SetText($"${_money}");
    private void SetCharacterMoveable()
    {
        if (Cursor.visible)
        {
            _controller.CanControlCallback();
            return;
        }
        _controller.CantControlCallback();
    }

    private void SetInteractable(bool value) => _isInteractableToMenu = value;
    #endregion
}