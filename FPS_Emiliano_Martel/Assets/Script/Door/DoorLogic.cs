using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField] private ActivePanel _active;

    public Action<bool> isOpen;

    private void OnEnable()
    {
        _active.isClicked += OpenLogic;
    }

    private void OnDisable()
    {
        _active.isClicked -= OpenLogic;
    }

    private void Awake()
    {
        if (!_active)
        {
            Debug.LogError($"{name}: Active panel is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void OpenLogic()
    {

    }

    [ContextMenu("Open door")]
    private void OpenDoor()
    {
        isOpen?.Invoke(true);
    }

    [ContextMenu("Close door")]
    private void CloseDoor()
    {
        isOpen?.Invoke(false);
    }
}
