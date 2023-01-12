using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Desk : ComputerPart
{

    [TabGroup("Desk")] public Dictionary<PartType, ComputerPart> _computerParts;

    private int _itemQuantityOnDesk;
    private readonly int _necessaryItemQuantity = 5;
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
}