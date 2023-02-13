using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MyComputer : MonoBehaviour, IInteractable
{

    [SerializeField] private Vector3 _sittingPosition, _sittingRotation;


    [SerializeField] private GameObject _openingPanel, _openPanel;


    private bool _canInteractable = true;
    private class SitVariables
    {
        public Character_Controller characterController;

        public Transform cameraTransform, oldCameraParent;
        public Vector3 firstCameraLocalPosition, firstCameraLocalRotation;
    }

    public void Interact(params object[] parameters)
    {
        if (!_canInteractable) return;

        _canInteractable = false;

        Transform cameraTransform = parameters[0] as Transform;

        Transform oldCameraParent = cameraTransform.parent;


        Vector3 firstCameraLocalPosition = cameraTransform.localPosition;
        Vector3 firstCameraLocalRotation = cameraTransform.localEulerAngles;


        Character_Controller characterController = (parameters[0] as Transform)?.GetComponentInParent<Character_Controller>();

        characterController.CantControlCallback?.Invoke();

        cameraTransform.SetParent(transform);


        cameraTransform.DOLocalMove(_sittingPosition, .50f);
        cameraTransform.DOLocalRotate(_sittingRotation, .50f);

        SitVariables tempVariables = new()
        {
            characterController = characterController,
            cameraTransform = cameraTransform,
            oldCameraParent = oldCameraParent,
            firstCameraLocalPosition = firstCameraLocalPosition,
            firstCameraLocalRotation = firstCameraLocalRotation
        };

        oldCameraParent.gameObject.SetActive(false);

        StartCoroutine(nameof(CO_ObservInput), tempVariables);
    }

    private IEnumerator CO_ObservInput(SitVariables sitVariables)
    {
        bool isFocused = false;

        yield return new WaitForSecondsRealtime(1);

        while (!Input.GetKeyDown(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.Return)) StartCoroutine(nameof(CO_OpenComputer));
            if (Input.GetKeyDown(KeyCode.F))
            {
                Vector3 endValue = isFocused ? _sittingPosition : _sittingPosition + Vector3.forward * 0.75f;
                isFocused = !isFocused;

                sitVariables.cameraTransform.DOKill();
                sitVariables.cameraTransform.DOLocalMove(endValue, 0.50f).SetEase(Ease.InOutBack);
            }

            yield return null;
        }

        SitUp(sitVariables);
    }

    private IEnumerator CO_OpenComputer()
    {
        if (_openingPanel.activeSelf) yield break;

        _openingPanel.SetActive(true);

        yield return new WaitForSecondsRealtime(2);

        _openingPanel.SetActive(false);

        _openPanel.SetActive(true);
    }

    private void SitUp<T>(T sitVariables) where T : SitVariables
    {
        sitVariables.cameraTransform.SetParent(sitVariables.oldCameraParent);

        sitVariables.oldCameraParent.gameObject.SetActive(true);


        sitVariables.cameraTransform.DOLocalMove(sitVariables.firstCameraLocalPosition, .50f);
        sitVariables.cameraTransform.DOLocalRotate(sitVariables.firstCameraLocalRotation, .50f).OnComplete(() => { sitVariables.characterController.CanControlCallback?.Invoke(); });

        _canInteractable = true;
    }
}