using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class CustomerStateManager : MonoBehaviour
{
    public Dictionary<System.Type, State> _states;

    private void Awake() => Initialize();

    private void Initialize()
    {
        if (_states == null) _states = new(); else _states.Clear();

        foreach (State state in GetComponentsInChildren<State>())
            if (!_states.ContainsKey(state.GetType())) _states.Add(state.GetType(), state);
    }
}