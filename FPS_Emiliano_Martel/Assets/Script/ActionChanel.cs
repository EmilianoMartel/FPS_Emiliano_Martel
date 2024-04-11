using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public struct ActionConfig
{
    [field: SerializeField] public bool listenerEvent { get; set; }
    [field: SerializeField] public bool eventLog { get; set; }
}

public abstract class ActionChanel<T> : ScriptableObject
{
    [SerializeField] private ActionConfig _config;
    private Action<T> _event = delegate { };

    public void Sucription(Action<T> action) 
    {
        _event += action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was suscribed at Event.");
        }
    }

    public void Unsuscribe(Action<T> action)
    {
        _event += action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was unsuscribed at Event.");
        }
    }

    public void InvokeEvent(T data)
    {
        _event?.Invoke(data);
        if (_config.eventLog)
        {
            Debug.Log($"{name}: The event was invoked.");
        }
    }
}
