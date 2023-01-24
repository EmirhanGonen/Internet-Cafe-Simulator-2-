using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    private Desk _desk;
    public Desk GetDesk() => _desk;

    private DeskCanvas _deskCanvas;

    public delegate void InfoForCustomersHandler();

    public InfoForCustomersHandler InfoForCustomersCallback;

    private void Awake() => SetVariables();

    private void SetVariables()
    {
        _desk = GetComponentInParent<Desk>();
        _deskCanvas = transform.parent.GetComponentInChildren<DeskCanvas>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out ComputerPart computerPart)) return;

        if (computerPart.transform.parent) return;

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

    public void RegisterMember(PartType partType, ComputerPart computerPart)
    {
        _desk.Register(partType, computerPart);
        InfoForCustomersCallback?.Invoke();
        CheckDeskIsAvailable();
    }
    public void UnRegisterMember(PartType key)
    {
        _desk.UnRegister(key);
        InfoForCustomersCallback?.Invoke();
    }



    public void SetNotAvailable() => ListHolder.Instance.AvailableDesks.Remove(this); //ilk önce kendini cýkartýcak baþka birisi gelmesin diye müþteri oturuncada masa saniyesini ayarlýcak
    public void UsedByCustomer()
    {
        // Desk Manager haber alcak customer bana geliyorum dedi masamý available listesinden cýkartýp notavailable listesine alýcam

        //ListHolder.Instance.NotavailableDesk.Add(this);

        _desk.UsedByCustomer(); //Müþteri masadan kalktýðý zaman çaðýrýyor
        SetMonitorOff();
    }

    [Button("SetMonitor")]
    public void SetMonitorScreen() => StartCoroutine(nameof(CO_SetMonitorScreen));

    public IEnumerator CO_SetMonitorScreen()
    {
        yield return new WaitForSeconds(0.50f);

        MeshRenderer _monitorMeshRenderer = _desk._computerParts[PartType.Monitor].gameObject.GetComponent<MeshRenderer>();

        Material[] materials = _monitorMeshRenderer.materials;

        materials[1] = MaterialHolder.Instance.GetRandomMonitorMaterial();

        _monitorMeshRenderer.materials = materials;

        SetMonitorScreen();
    }
    private void SetMonitorOff()
    {
        StopCoroutine(nameof(CO_SetMonitorScreen));

        MeshRenderer _monitorMeshRenderer = _desk._computerParts[PartType.Monitor].gameObject.GetComponent<MeshRenderer>();

        Material[] materials = _monitorMeshRenderer.materials;

        materials[1] = MaterialHolder.Instance.Monitor_Off;

        _monitorMeshRenderer.materials = materials;
    }

    public void CheckDeskIsAvailable()
    {
        if ((_desk.IsCompleted() & _desk.IsAvailable()) & !ListHolder.Instance.AvailableDesks.Contains(this)) ListHolder.Instance.AvailableDesks.Add(this);
    }
}