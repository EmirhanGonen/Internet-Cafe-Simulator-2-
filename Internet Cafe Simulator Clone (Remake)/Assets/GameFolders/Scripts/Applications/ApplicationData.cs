using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "ScriptableObjects/Application")]
public class ApplicationData : ScriptableObject
{
    public virtual void ActiveManage(GameObject panel) => panel.SetActive(!panel.activeSelf);

    public void FullScreenButton(GameObject gameObject) => gameObject.transform.DOScale(gameObject.transform.localScale.x == 0.50f ? 1f : 0.50f, 0.20f);
}