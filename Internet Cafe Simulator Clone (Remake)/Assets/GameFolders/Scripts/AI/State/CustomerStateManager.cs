using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class CustomerStateManager : SerializedMonoBehaviour
{
    public Dictionary<System.Type, State> _states;

    private void Awake() => Initialize();

    [Button("Initialize")]
    private void Initialize()
    {
        if (_states == null) _states = new(); else _states.Clear();

        //_states.AddRange(GetComponentsInChildren<State>());
        foreach (State state in GetComponentsInChildren<State>())
        {
            if (!_states.ContainsKey(state.GetType())) _states.Add(state.GetType(), state);

            Debug.Assert(_states.ContainsKey(state.GetType()), "Has Key");
        }
    }
}