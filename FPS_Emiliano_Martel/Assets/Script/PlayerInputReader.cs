using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private BoolChanelSo _isTriggerEvent;
    [SerializeField] private Vector2Channel _directionEvent;
    [SerializeField] private Vector2Channel _lookEvent;
    [SerializeField] private EmptyAction _jumpEvent;
    [SerializeField] private BoolChanelSo _sprintEvent;
    [SerializeField] private EmptyAction _reloadEvent;
    [SerializeField] private EmptyAction _interactEvent;

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        _directionEvent.InvokeEvent(inputContext.ReadValue<Vector2>());
    }

    public void SetJump(InputAction.CallbackContext inputContext)
    {
        _jumpEvent.InvokeEvent();
    }

    public void SetLook(InputAction.CallbackContext inputContext)
    {
        _lookEvent.InvokeEvent(inputContext.ReadValue<Vector2>());
    }

    public void SetSprint(InputAction.CallbackContext inputContext)
    {
        _sprintEvent.InvokeEvent(inputContext.ReadValueAsButton());
    }

    public void SetShoot(InputAction.CallbackContext inputContext)
    {
        _isTriggerEvent.InvokeEvent(inputContext.ReadValueAsButton());
    }

    public void SetReload(InputAction.CallbackContext inputContext)
    {
        _reloadEvent.InvokeEvent();
    }

    public void SetInteract(InputAction.CallbackContext inputContext)
    {
        _interactEvent.InvokeEvent();
    }
}