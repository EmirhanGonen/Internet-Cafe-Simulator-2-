using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class DeskCanvas : MonoBehaviour
{

    [FoldoutGroup("Icons"), SerializeField] private Image _caseIcon;
    [FoldoutGroup("Icons"), SerializeField] private Image _monitorIcon;
    [FoldoutGroup("Icons"), SerializeField] private Image _chairIcon;
    [FoldoutGroup("Icons"), SerializeField] private Image _keyboardIcon;
    [FoldoutGroup("Icons"), SerializeField] private Image _mouseIcon;


    [FoldoutGroup("Marks"), SerializeField] private Sprite _greenTickMark;
    [FoldoutGroup("Marks"), SerializeField] private Sprite _redTickMark;


    private Camera _camera;
    private Canvas _canvas;

    private Desk _desk;

    private void Awake() => SetVariables();

    private void SetVariables()
    {
        _camera = Camera.main;
        _canvas = GetComponent<Canvas>();
        _desk = GetComponentInParent<Desk>();
    }

    private void Update() => LookAt();

    private void LookAt() => transform.LookAt(_camera.transform.position);

    public void RegisteredMember(PartType key, bool isRegistered)
    {
        switch (key)
        {
            case PartType.ComputerCase: _caseIcon.sprite = isRegistered ? _greenTickMark : _redTickMark; break;
            case PartType.Monitor: _monitorIcon.sprite = isRegistered ? _greenTickMark : _redTickMark; break;
            case PartType.Chair: _chairIcon.sprite = isRegistered ? _greenTickMark : _redTickMark; break;
            case PartType.Keyboard: _keyboardIcon.sprite = isRegistered ? _greenTickMark : _redTickMark; break;
            case PartType.Mouse: _mouseIcon.sprite = isRegistered ? _greenTickMark : _redTickMark; break;
        }
        Debug.Log($"Desk Complete State : {_desk.IsCompleted()}");
        CheckCanvasActive();
    }

    private void CheckCanvasActive() => _canvas.enabled = !_desk.IsCompleted();
}