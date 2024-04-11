using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channels/Empty Channels", fileName = "EmptyChannel")]
public class EmptyAction : ScriptableObject
{
    [SerializeField] private ActionConfig _config;
    private Action _event = delegate { };

    public void Sucription(Action action)
    {
        _event += action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was suscribed at Event.");
        }
    }

    public void Unsuscribe(Action action)
    {
        _event += action;
        if (_config.listenerEvent)
        {
            Debug.Log($"{name}: A listener({action}) was unsuscribed at Event.");
        }
    }

    public void InvokeEvent()
    {
        _event?.Invoke();
        if (_config.eventLog)
        {
            Debug.Log($"{name}: The event was invoked.");
        }
    }
}
