using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] private BoolChanelSo _startedGame;

    private void OnEnable()
    {
        _startedGame?.Sucription(PauseGame);
    }

    private void OnDisable()
    {
        _startedGame?.Unsuscribe(PauseGame);
    }

    private void Awake()
    {
        if (!_startedGame)
        {
            Debug.LogError($"{name}: Started Game Channel is null.\nCheck and assigned one.\nDisabling component.");
            enabled= false;
            return;
        }
    }

    private void PauseGame(bool isPlaying)
    {
        if(isPlaying)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
