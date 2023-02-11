using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="ScriptableObjects/Categorize/Data")]
public class CategorizeData : ScriptableObject
{
    [SerializeField] private List<ItemData> _datas;

    [HideInInspector] public List<ItemData> Datas { get => _datas; }
}