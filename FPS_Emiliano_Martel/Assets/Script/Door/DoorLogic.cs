using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour, IInteract
{
    public Action<bool> isOpen = delegate { };

    private bool _actualState = false;

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

    public void InteractiveMoment()
    {
        _actualState = !_actualState;
        isOpen?.Invoke(_actualState);
    }
}