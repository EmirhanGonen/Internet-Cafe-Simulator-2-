using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class ComputerPart : Item
{
    #region Serialized Variables
    [FoldoutGroup("Computer Part Variables")]
    [FoldoutGroup("Computer Part Variables/ComputerType")] public PartType Type;
    [FoldoutGroup("Computer Part Variables/ComputerType")] public string _collisionTag;
    [FoldoutGroup("Computer Part Variables/Place Positions"), SerializeField] private Vector3 _placeLocalPosition, _placeLocalRotation; //While Place
    [FoldoutGroup("Computer Part Variables/RotatePower"), SerializeField] private Vector3 _rotateValue; //While Place
    #endregion
    #region Private Variables

    protected bool _isCollision = true;
    private MeshRenderer _meshRenderer;
    private Material _ourMaterial;
    private Transform _playerTransform;
    #endregion

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _ourMaterial = _meshRenderer.material;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override IEnumerator Use()
    {
        StartCoroutine(base.Use());

        yield return new WaitForEndOfFrame();

        transform.rotation = Quaternion.Euler(_placeLocalRotation);

        SetParent(null);

        while (!Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _raycastHit, 5, _layerMask);

            if (_raycastHit.collider) transform.position = _raycastHit.point + _placeLocalPosition;
            else transform.position = _playerTransform.position + _playerTransform.forward;



            _meshRenderer.material = _isCollision ? MaterialHolder.Instance.RedPreview : MaterialHolder.Instance.GreenPreview;

            if (Input.GetKey(KeyCode.Q)) SetRotation(-_rotateValue);
            if (Input.GetKey(KeyCode.E)) SetRotation(_rotateValue);

            yield return null;
        }
        if (_isCollision) { StartCoroutine(nameof(Use)); yield break; }

        SetCompenentVariables(null, false, false, false);
        isUsed = false;

        _meshRenderer.material = _ourMaterial;
    }
    private void SetCompenentVariables(Transform parent, bool isCarryItem, bool rigidbodyKinematic, bool colliderTrigger)
    {
        SetParent(parent);

        SetIsCarryItem(isCarryItem);

        SetRigidbodyKinematic(rigidbodyKinematic);

        SetColliderTrigger(colliderTrigger);
    }

    private void SetRotation(Vector3 rotateValue) => transform.Rotate(rotateValue);

    private void OnTriggerEnter(Collider collision)
    {
        if (!isUsed) return;

        if (collision.CompareTag(_collisionTag))
        {
            CheckIsCollision(out _isCollision, false);
            return;
        }
        CheckIsCollision(out _isCollision, true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isUsed) return;
        if (other.CompareTag(_collisionTag))
        {
            CheckIsCollision(out _isCollision, true);
            return;
        }
        CheckIsCollision(out _isCollision, false);
    }
    protected void CheckIsCollision(out bool isCollision, bool value) => isCollision = value;
}

public enum PartType
{
    ComputerCase,
    Monitor,
    Chair,
    Keyboard,
    Mouse,
    Desk
}