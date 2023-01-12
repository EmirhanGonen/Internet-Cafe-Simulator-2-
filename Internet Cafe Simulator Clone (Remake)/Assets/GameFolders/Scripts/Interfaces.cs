using System.Collections;
using UnityEngine;

public interface IInteractable
{
    public void Interact(params object[] parameters);

}
public interface ICarryable
{
    public IEnumerator Carry(Transform parent);
    public void Drop();

}

public interface IUsable
{
    public abstract IEnumerator Use();
}

public interface IInfo
{
    public string Name { get; }
}