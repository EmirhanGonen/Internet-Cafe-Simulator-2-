using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Bat : Item
{
    private Animator _animator;

    private int _animationHash;

    private const string ANIMATION_NAME = "Attack";

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _animator.enabled = false;

        _animationHash = Animator.StringToHash(ANIMATION_NAME);
    }

    public override IEnumerator Use()
    {
        if (_animator.enabled) yield break;

        StartCoroutine(base.Use());

        _animator.enabled = true;

        _animator.Play(_animationHash);

        isUsed = false;

        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit _raycastHit, 5, _layerMask);


        if (_raycastHit.collider)
        {
            IDamagable damagable = _raycastHit.collider.transform.parent ? _raycastHit.collider.transform.parent.GetComponentInParent<IDamagable>() : _raycastHit.collider.GetComponent<IDamagable>();
            if (damagable != null) GiveDamage(damagable);
        }

        yield return new WaitForSecondsRealtime(0.65f);

        // transform.SetLocalPositionAndRotation(_carryLocalPosition , Quaternion.Euler(_carryLocalRotation));

        _animator.enabled = false;
    }

    private async void GiveDamage(IDamagable damagable)
    {
        await Task.Delay(500);
        damagable.TakeDamage(transform.parent.parent);
    }

    public override void Drop()
    {
        _animator.enabled = false;
        base.Drop();
    }
}