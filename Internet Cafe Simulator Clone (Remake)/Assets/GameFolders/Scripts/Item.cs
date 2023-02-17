using System.Collections;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class Item : MonoBehaviour, IInteractable, ICarryable, IUsable, IInfo
{
    #region Serialized Variables

    [FoldoutGroup("ItemVariables")]
    [FoldoutGroup("ItemVariables/Carry Positions"), SerializeField] protected Vector3 _carryLocalPosition, _carryLocalRotation; //While Carryed
    [FoldoutGroup("ItemVariables/Layer Mask"), SerializeField] protected LayerMask _layerMask;
    [FoldoutGroup("ItemVariables"), SerializeField] private string _name;
    #endregion
    #region Protected & Private Variables

    protected RaycastHit _raycastHit;
    protected Camera _camera;
    private Rigidbody _rigidbody;
    private Collider _collider;

    protected bool isUsed;

    public string Name { get => _name; }

    private Vector3 _localScale;

    private bool _canDrop;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _camera = Camera.main;
        _localScale = transform.localScale;
    }
    public void Interact(params object[] parametres) => StartCoroutine(nameof(Carry), parametres[0] as Transform);
    public virtual IEnumerator Carry(Transform parent)
    {
        _canDrop = false;

        SetIsCarryItem(true);

        SetRigidbodyKinematic(true);
        SetColliderTrigger(true);

        SetParent(parent);

        transform.DOLocalRotate(_carryLocalRotation, .20f, RotateMode.Fast).SetEase(Ease.Linear);
        transform.DOLocalMove(_carryLocalPosition, .50f).SetEase(Ease.InOutBack).OnComplete(()=> { _canDrop = true; });

        while (!isUsed)
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(nameof(Use));
            if (Input.GetMouseButtonDown(1)) Drop();
            yield return null;
        }
    }
    public virtual void Drop()
    {
        if (!_canDrop) return;

        transform.SetLocalPositionAndRotation(_carryLocalPosition, Quaternion.Euler(_carryLocalRotation));

        SetRigidbodyKinematic(false);
        SetColliderTrigger(false);

        _rigidbody.AddForce(100 * Time.deltaTime * transform.parent.forward, ForceMode.Impulse);

        SetParent(null);


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