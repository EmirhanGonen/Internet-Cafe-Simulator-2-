using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CourierStateManager : MonoBehaviour
{
    private Dictionary<System.Type, CourierState> _states = new();

    public Dictionary<System.Type, CourierState> GetStates => _states;

    private void Awake() => Initialize();

    private void Initialize()
    {
        foreach (CourierState state in GetComponentsInChildren<CourierState>())
            if (!_states.ContainsKey(state.GetType())) _states.Add(state.GetType(), state);
    }
}