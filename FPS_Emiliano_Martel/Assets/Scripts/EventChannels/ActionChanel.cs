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
        _event -= action;
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

public abstract class ActionChanel<T1, T2> : ScriptableObject
{
    [SerializeField] private ActionConfig _config;
    private Action<T1, T2> _event = delegate { };

    public void Sucription(Action<T1, T2> action)
    {
        _event += action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was suscribed at Event.");
        }
    }

    public void Unsuscribe(Action<T1, T2> action)
    {
        _event -= action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was unsuscribed at Event.");
        }
    }

    public void InvokeEvent(T1 data,T2 data2)
    {
        _event?.Invoke(data,data2);
        if (_config.eventLog)
        {
            Debug.Log($"{name}: The event was invoked.");
        }
    }
}

public abstract class ActionChanel<T1,T2,T3> : ScriptableObject
{
    [SerializeField] private ActionConfig _config;
    private Action<T1, T2, T3> _event = delegate { };

    public void Sucription(Action<T1, T2, T3> action)
    {
        _event += action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was suscribed at Event.");
        }
    }

    public void Unsuscribe(Action<T1, T2, T3> action)
    {
        _event -= action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was unsuscribed at Event.");
        }
    }

    public void InvokeEvent(T1 data1, T2 data2, T3 data3)
    {
        _event?.Invoke(data1,data2,data3);
        if (_config.eventLog)
        {
            Debug.Log($"{name}: The event was invoked.");
        }
    }
}
