using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private string _pauseMenu = "Pause";
    [SerializeField] private string _gameUi = "Play";
    [Header("Channels")]
    [SerializeField] private BoolChanelSo _isTriggerEvent;
    [SerializeField] private Vector2Channel _directionEvent;
    [SerializeField] private Vector2Channel _lookEvent;
    [SerializeField] private EmptyAction _jumpEvent;
    [SerializeField] private BoolChanelSo _sprintEvent;
    [SerializeField] private EmptyAction _reloadEvent;
    [SerializeField] private EmptyAction _interactEvent;
    [SerializeField] private StringChannel _menuNameEvent;
    [SerializeField] private BoolChanelSo _startedGame;

    private bool _paused = false;
    private bool _isPlaying = false;

    private void OnEnable()
    {
        _startedGame?.Sucription(HandleStartedGame);
    }

    private void OnDisable()
    {
        _startedGame?.Unsuscribe(HandleStartedGame);
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        if (_paused)
            return;

        _directionEvent.InvokeEvent(inputContext.ReadValue<Vector2>());
    }

    public void SetJump(InputAction.CallbackContext inputContext)
    {
        if (_paused)
            return;

        _jumpEvent.InvokeEvent();
    }

    public void SetLook(InputAction.CallbackContext inputContext)
    {
        if (_paused)
            return;

        _lookEvent.InvokeEvent(inputContext.ReadValue<Vector2>());
    }

    public void SetSprint(InputAction.CallbackContext inputContext)
    {
        if (_paused)
            return;

        _sprintEvent.InvokeEvent(inputContext.ReadValueAsButton());
    }

    public void SetShoot(InputAction.CallbackContext inputContext)
    {
        if (_paused)
            return;

        _isTriggerEvent.InvokeEvent(inputContext.ReadValueAsButton());
    }

    public void SetReload(InputAction.CallbackContext inputContext)
    {
        if (_paused)
            return;

        _reloadEvent.InvokeEvent();
    }

    public void SetInteract(InputAction.CallbackContext inputContext)
    {
        if (_paused)
            return;

        _interactEvent.InvokeEvent();
    }

    public void SetPause(InputAction.CallbackContext inputContext)
    {
        if(_isPlaying && inputContext.started && !_paused && _isPlaying)
        {
            _menuNameEvent?.InvokeEvent(_pauseMenu);

            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            _paused = true;
        }else if (_isPlaying && inputContext.started && _paused)
        {
            _menuNameEvent?.InvokeEvent(_gameUi);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            _paused = false;
        }
    }

    private void HandleStartedGame(bool isPlaying)
    {
        _isPlaying = isPlaying;
        if (_isPlaying)
            _paused = false;
    }
}