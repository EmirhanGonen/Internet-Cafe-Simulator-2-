using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Categorize/Button")]
public class CategorizeButton : ScriptableObject
{
    [SerializeField] private CategorizeData _data;

    public void Categorize()
    {
        ListHolder _listHolder = ListHolder.Instance;

        foreach (ItemTemplate itemTemplate in _listHolder.itemTemplate)
        {
            itemTemplate.GetCategorize(_data.Datas[_listHolder.itemTemplate.IndexOf(itemTemplate)]);
        }  
    }
}