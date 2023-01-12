using System.Collections;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(MeshCollider), typeof(Rigidbody))]
public abstract class Item : SerializedMonoBehaviour, IInteractable, ICarryable, IUsable
{
    #region Serialized Variables

    [FoldoutGroup("ItemVariables")]
    [FoldoutGroup("ItemVariables/Carry Positions"), SerializeField] private Vector3 _carryLocalPosition, _carryLocalRotation; //While Carryed
    [FoldoutGroup("ItemVariables/Layer Mask"), SerializeField] protected LayerMask _layerMask;

    #endregion
    #region Protected & Private Variables

    protected RaycastHit _raycastHit;
    protected Camera _camera;
    private Rigidbody _rigidbody;
    private Collider _collider;

    protected bool isUsed;

    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _camera = Camera.main;
    }
    public void Interact(params object[] parametres) => StartCoroutine(nameof(Carry), parametres[0] as Transform);
    public IEnumerator Carry(Transform parent)
    {
        SetIsCarryItem(true);

        SetRigidbodyKinematic(true);
        SetColliderTrigger(true);

        SetParent(parent);

        transform.DOLocalMove(_carryLocalPosition, .50f).SetEase(Ease.InOutBack);
        transform.DOLocalRotate(_carryLocalRotation, .20f, RotateMode.Fast).SetEase(Ease.Linear);

        while (!isUsed)
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(nameof(Use));
            if (Input.GetMouseButtonDown(1)) Drop();
            yield return null;
        }
    }
    public void Drop()
    {
        transform.DOKill();

        transform.SetLocalPositionAndRotation(_carryLocalPosition, Quaternion.Euler(_carryLocalRotation));

        Vector3 forceDirection = transform.parent ? transform.parent.forward : transform.forward;

        SetRigidbodyKinematic(false);
        SetColliderTrigger(false);

        SetParent(null);

        _rigidbody.AddForce(500 * Time.deltaTime * forceDirection, ForceMode.Impulse);

        SetIsCarryItem(false);
        isUsed = false;
        StopAllCoroutines();
    }

    public virtual IEnumerator Use()
    {
        isUsed = true;
        yield return new WaitForEndOfFrame();
    }


    protected void SetParent(Transform parent) => transform.SetParent(parent);
    protected void SetRigidbodyKinematic(bool state) => _rigidbody.isKinematic = state;
    protected void SetColliderTrigger(bool state) => _collider.isTrigger = state;
    protected void SetIsCarryItem(bool state) => Interact_Manager.Instance.SetIsCarry(state);
}