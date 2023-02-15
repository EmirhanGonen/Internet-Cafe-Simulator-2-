using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Categorize/Button")]
public class CategorizeButton : ScriptableObject
{
    [SerializeField] private CategorizeData _data;

    public void Categorize()
    {
        ListHolder _listHolder = ListHolder.Instance;

        for (int i = _data.Datas.Count; i < ListHolder.Instance.itemTemplate.Count; i++)
        {
            if (!_listHolder.itemTemplate[i].gameObject.activeSelf) break;

            _listHolder.itemTemplate[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _data.Datas.Count; i++)
        {
            _listHolder.itemTemplate[i].gameObject.SetActive(true);
            _listHolder.itemTemplate[i].GetCategorize(_data.Datas[i]);
        }
    }
}