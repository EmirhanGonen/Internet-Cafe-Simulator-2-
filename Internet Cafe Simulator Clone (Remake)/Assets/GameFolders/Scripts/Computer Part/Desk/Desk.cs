using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : ComputerPart
{
    //available state acabilirm

    [TabGroup("Desk")] public Dictionary<PartType, ComputerPart> _computerParts = new();

    private int _itemQuantityOnDesk;
    private readonly int _necessaryItemQuantity = 5;

    private readonly float _reAvailableDuration = 15.00f;
    private float _reAvailableTime;

    public void Register(PartType partType, ComputerPart computerPart)
    {

        if (_computerParts.ContainsKey(partType) | partType == PartType.Desk) return;

        _computerParts.Add(partType, computerPart);
    }

    public void UnRegister(PartType key)
    {
        _computerParts.Remove(key);
    }

    public bool IsCompleted()
    {
        _itemQuantityOnDesk = 0;
        foreach (KeyValuePair<PartType, ComputerPart> type in _computerParts)
        {
            if (type.Key == PartType.ComputerCase | type.Key == PartType.Monitor | type.Key == PartType.Chair |
                type.Key == PartType.Keyboard | type.Key == PartType.Mouse) _itemQuantityOnDesk++;
        }
        return _itemQuantityOnDesk == _necessaryItemQuantity;
    }

    public void UsedByCustomer()
    {
        float a = _reAvailableTime;
        _reAvailableTime = Time.time + _reAvailableDuration;
        StartCoroutine(nameof(CO_CheckDeskIsAvailable));
        Debug.Log($"Eski Time {a } Yeni Time {_reAvailableTime }");
    }

    private IEnumerator CO_CheckDeskIsAvailable()
    {
        float waitDiffrent = _reAvailableTime - Time.time;

        Debug.Log(waitDiffrent);

        yield return new WaitForSeconds(waitDiffrent);

        GetComponentInChildren<DeskManager>().CheckDeskIsAvailable();
    }

    public bool IsAvailable() => Time.time > _reAvailableTime;
}