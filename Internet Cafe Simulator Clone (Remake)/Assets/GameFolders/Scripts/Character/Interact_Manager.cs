using UnityEngine;

public class Interact_Manager : Singleton<Interact_Manager>
{
    [SerializeField] private InteractLogic _interact;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private LayerMask _interactLayerMask;
    [SerializeField] private LayerMask _infoLayerMask;

    [SerializeField, Range(1, 100)] private float _rayDistance;

    private RaycastHit _interactRaycastHit;
    private RaycastHit _infoRaycast;

    private Camera _camera;


    private bool _isCarryItem;

    private delegate void InteractHandle();
    private InteractHandle InteractCallBack;

    private void OnEnable()
    {
        InteractCallBack += CheckInteract;
    }
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }
    public void Update()
    {
        CheckInfo();
        if (Input.GetKeyDown(_interactKey) & !_isCarryItem) InteractCallBack?.Invoke();
    }

    private void SendRay(out RaycastHit hit, LayerMask layerMask) => Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _rayDistance, layerMask);
    private void CheckInteract()
    {
        SendRay(out _interactRaycastHit, layerMask: _interactLayerMask);

        if (!_interactRaycastHit.collider) return;

        if (_interactRaycastHit.collider.TryGetComponent(out IInteractable interactable)) _interact.Interact(interactable, _camera.transform);
    }

    private void CheckInfo()
    {
        SendRay(out _infoRaycast, _infoLayerMask);

        if (!_infoRaycast.collider) { _interact.SetInfoPanel(string.Empty); return; } 

        if (!_infoRaycast.collider.TryGetComponent(out IInfo info)) return;

        _interact.SetInfoPanel(info.Name);
    }

    public void SetIsCarry(bool state) => _isCarryItem = state;
}