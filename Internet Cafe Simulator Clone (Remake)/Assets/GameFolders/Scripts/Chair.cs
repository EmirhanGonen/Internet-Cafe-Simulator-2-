using UnityEngine;

public class Chair : ComputerPart
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Desk desk)) return;
        DeskManager deskManager  = desk.GetComponentInChildren<DeskManager>();
        deskManager.RegisterMember(Type, this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Desk desk)) return;

        DeskManager deskManager = desk.GetComponentInChildren<DeskManager>();

        deskManager.UnRegisterMember(Type);
    }
}