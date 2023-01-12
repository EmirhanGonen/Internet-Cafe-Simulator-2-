using UnityEngine;

public class DeskManager : MonoBehaviour
{
    private Desk _desk;
    private DeskCanvas _deskCanvas;

    private void Awake() => SetVariables();

    private void SetVariables()
    {
        _desk = GetComponentInParent<Desk>();
        _deskCanvas = transform.parent.GetComponentInChildren<DeskCanvas>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out ComputerPart computerPart)) return;
        RegisterMember(computerPart.Type, computerPart);
        _deskCanvas.RegisteredMember(computerPart.Type, true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ChairCollision")) return;
        Chair chair = other.transform.parent.GetComponent<Chair>();

        RegisterMember(chair.Type, chair);
        _deskCanvas.RegisteredMember(PartType.Chair, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("ChairCollision")) return;
        Chair chair = other.transform.parent.GetComponent<Chair>();

        RegisterMember(chair.Type, chair);
        _deskCanvas.RegisteredMember(PartType.Chair, false);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out ComputerPart computerPart)) return;
        UnRegisterMember(computerPart.Type);
        _deskCanvas.RegisteredMember(computerPart.Type, false);
    }

    public void RegisterMember(PartType partType, ComputerPart computerPart) => _desk.Register(partType, computerPart);
    public void UnRegisterMember(PartType key) => _desk.UnRegister(key);
}