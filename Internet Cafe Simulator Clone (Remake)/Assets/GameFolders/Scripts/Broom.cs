using System.Collections;
using UnityEngine;

public class Broom : Item
{
    private Animator _animator;

    private const string ANIMATION_ID = "Clean";

    private int _animationHash;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animationHash = Animator.StringToHash(ANIMATION_ID);

        _animator.enabled = false;
    }

    public override IEnumerator Use()
    {
        if (_animator.enabled) yield break;

        StartCoroutine(base.Use());


        _animator.enabled = true;
        _animator.Play(_animationHash);

        isUsed = false;
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit _raycastHit, 3, _layerMask);

        if (_raycastHit.collider) if (_raycastHit.collider.TryGetComponent(out Garbage garbage)) garbage.GetCleaned();

        yield return new WaitForSeconds(1.25f);

        _animator.enabled = false;
    }
    public override void Drop()
    {
        _animator.enabled = false;
        base.Drop();
    }
}