using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;

public class ComputerPart : Item
{
    [SerializeField] private Vector3 _placeLocalPosition, _placeLocalRotation; //While Place
    private bool isCollision;
    private MeshRenderer _meshRenderer;
    private Material _ourMaterial;

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

            if (Input.GetKey(KeyCode.Q)) SetRotation(-5);
            if (Input.GetKey(KeyCode.E)) SetRotation(5);

            yield return null;
        }
        if (isCollision) { StartCoroutine(nameof(Use)); yield break; }

        SetCompenentVariables();
        isUsed = false;

        _meshRenderer.material = _ourMaterial;
    }
    private void SetCompenentVariables()
    {
        SetParent(null);

        SetIsCarryItem(false);

        SetRigidbodyKinematic(false);

        SetColliderTrigger(false);

    }

    private void SetRotation(int value) => transform.Rotate(new(0, 0, transform.rotation.z + value));

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerMask) return;
        CheckIsCollision(out isCollision, true);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _layerMask) return;
        CheckIsCollision(out isCollision, false);
    }
    private void CheckIsCollision(out bool isCollision, bool value) => isCollision = value;
}