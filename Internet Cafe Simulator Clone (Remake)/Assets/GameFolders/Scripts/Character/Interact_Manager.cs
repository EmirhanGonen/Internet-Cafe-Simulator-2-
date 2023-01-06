using UnityEngine;


public class Interact_Manager : Singleton<Interact_Manager>
{
    [SerializeField] private InteractLogic _interact;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField, Range(1, 100)] private float _rayDistance;

    private RaycastHit _raycastHit;

    private Camera _camera;


    private bool _isCarryItem;

    private delegate void InteractManager();
    private InteractManager InteractCallBack;

    private void OnEnable()
    {
        InteractCallBack += SendRay;
        InteractCallBack += CheckInteract;
    }
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }
    public void Update()
    {
        if (Input.GetKeyDown(_interactKey) & !_isCarryItem) InteractCallBack?.Invoke();
    }

    private void SendRay() => Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _raycastHit, _rayDistance, _layerMask);

    private void CheckInteract()
    {
        if (!_raycastHit.collider) return;

        if (_raycastHit.collider.TryGetComponent(out IInteractable interactable)) _interact.Interact(interactable, _camera.transform);
    }

    public void SetIsCarry(bool state) => _isCarryItem = state;
}