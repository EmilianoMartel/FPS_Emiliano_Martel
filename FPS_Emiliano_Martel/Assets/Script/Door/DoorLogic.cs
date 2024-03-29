using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public Action<bool> isOpen;

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
