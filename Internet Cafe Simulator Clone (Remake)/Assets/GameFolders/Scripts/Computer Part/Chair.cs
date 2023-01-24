using UnityEngine;

public class Chair : ComputerPart
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_collisionTag)) CheckIsCollision(out _isCollision, true);
        else CheckIsCollision(out _isCollision, false);

        if (!other.TryGetComponent(out Desk desk)) return;

        DeskManager deskManager = desk.GetComponentInChildren<DeskManager>();

        deskManager.RegisterMember(Type, this);
    }


    private void OnTriggerExit(Collider other)
    {
        CheckIsCollision(out _isCollision, false);

        if (!other.TryGetComponent(out Desk desk)) return;

        DeskManager deskManager = desk.GetComponentInChildren<DeskManager>();

        deskManager.UnRegisterMember(Type);
    }
}