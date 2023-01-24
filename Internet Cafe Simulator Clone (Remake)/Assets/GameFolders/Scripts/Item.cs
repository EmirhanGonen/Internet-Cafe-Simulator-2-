using System.Collections;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class Item : MonoBehaviour, IInteractable, ICarryable, IUsable, IInfo
{
    #region Serialized Variables

    [FoldoutGroup("ItemVariables")]
    [FoldoutGroup("ItemVariables/Carry Positions"), SerializeField] private Vector3 _carryLocalPosition, _carryLocalRotation; //While Carryed
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
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _camera = Camera.main;
        _localScale = transform.localScale;
    }
    public void Interact(params object[] parametres) => StartCoroutine(nameof(Carry), parametres[0] as Transform);
    public IEnumerator Carry(Transform parent)
    {
        SetIsCarryItem(true);

        SetRigidbodyKinematic(true);
        SetColliderTrigger(true);

        SetParent(parent);

        // transform.localScale = _localScale;

        //transform.SetLocalPositionAndRotation(transform.localPosition, Quaternion.identity);

        transform.DOLocalRotate(_carryLocalRotation, .20f, RotateMode.Fast).SetEase(Ease.Linear);
        transform.DOLocalMove(_carryLocalPosition, .50f).SetEase(Ease.InOutBack);

        //.localPosition = _carryLocalPosition;
        //transform.localRotation = Quaternion.Euler(_carryLocalRotation);

        while (!isUsed)
        {
            if (Input.GetMouseButtonDown(0)) StartCoroutine(nameof(Use));
            if (Input.GetMouseButtonDown(1)) Drop();
            yield return null;
        }
    }
    public void Drop()
    {
        //transform.DOKill();

        transform.SetLocalPositionAndRotation(_carryLocalPosition, Quaternion.Euler(_carryLocalRotation));

        Vector3 forceDirection = transform.parent ? transform.parent.forward : transform.forward; //�lk ald���m�z konuma �s�nl�yor ;

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