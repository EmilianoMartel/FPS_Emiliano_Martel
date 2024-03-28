using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private FirstPersonController _player;

    private void Awake()
    {
        if (!_player)
        {
            Debug.LogError($"{name}: FirstPersonController is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        _player.direction = inputContext.ReadValue<Vector2>();
    }

    public void SetJump(InputAction.CallbackContext inputContext)
    {
        _player.jump = true;
    }

    public void SetLook(InputAction.CallbackContext inputContext)
    {
        _player.lookRotation = inputContext.ReadValue<Vector2>();
    }

    public void SetSprint(InputAction.CallbackContext inputContext)
    {
        _player.sprint = inputContext.ReadValueAsButton();
    }

    public void SetShoot(InputAction.CallbackContext inputContext)
    {
        _player.shootEvent?.Invoke(inputContext.ReadValueAsButton());
    }

    public void SetReload(InputAction.CallbackContext inputContext)
    {
        _player.reloadEvent?.Invoke();
    }

    public void SetPickUp(InputAction.CallbackContext inputContext)
    {

    }
}