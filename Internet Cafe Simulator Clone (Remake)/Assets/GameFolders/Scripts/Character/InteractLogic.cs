using UnityEngine;

public class InteractLogic : MonoBehaviour
{
    //public void Interact(IInteractable interactable, Transform carryParent) => interactable.Interact(carryParent);
    public void Interact(IInteractable interactable, params object[] parameters) => interactable.Interact(parameters);

    public void SetInfoPanel(string objectName) => InfoUI.Instance.InfoCallback?.Invoke(objectName);
}