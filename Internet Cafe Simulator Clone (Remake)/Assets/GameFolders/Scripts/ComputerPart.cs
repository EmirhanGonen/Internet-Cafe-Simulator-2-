using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class ComputerPart : Item
{
    #region Serialized Variables
    [FoldoutGroup("Computer Part Variables")]
    [FoldoutGroup("Computer Part Variables/ComputerType")] public PartType Type;
    [FoldoutGroup("Computer Part Variables/Place Positions"), SerializeField] private Vector3 _placeLocalPosition, _placeLocalRotation; //While Place
    [FoldoutGroup("Computer Part Variables/RotatePower"), SerializeField] private Vector3 _rotateValue; //While Place
    #endregion
    #region Private Variables

    private bool isCollision;
    private MeshRenderer _meshRenderer;
    private Material _ourMaterial;

    #endregion

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _ourMaterial = _meshRenderer.material;
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
            _meshRenderer.material = isCollision ? MaterialHolder.Instance.RedPreview : MaterialHolder.Instance.GreenPreview;
;
            if (Input.GetKey(KeyCode.Q)) SetRotation(-_rotateValue);
            if (Input.GetKey(KeyCode.E)) SetRotation(_rotateValue);

            yield return null; ;
        }
        if (isCollision) { StartCoroutine(nameof(Use)); yield break; }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerMask) return;
        if (Type == PartType.Chair) Debug.Log("SA");
        CheckIsCollision(out isCollision, true);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _layerMask) return;
        CheckIsCollision(out isCollision, false);
    }
    private void CheckIsCollision(out bool isCollision, bool value) => isCollision = value;
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