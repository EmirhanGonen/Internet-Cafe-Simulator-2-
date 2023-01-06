using UnityEngine;

public class InteractLogic : MonoBehaviour
{
    public void Interact(IInteractable interactable , Transform carryParent)
    {
        interactable.Interact(carryParent);
    }
}