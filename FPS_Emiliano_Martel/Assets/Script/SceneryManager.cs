using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] private BoolChanelSo _startedGame;
    [SerializeField] private StringChannel _menuNameEvent;
    [SerializeField] private string _mainMenuName = "Menu";
    [SerializeField] private string _loadingUI = "Loading";
    [SerializeField] private int _level1Index = 2;
    [SerializeField] private int _menusIndex = 1;

    private void OnEnable()
    {
        _startedGame?.Sucription(HandlePlayedEvent);   
    }

    private void OnDisable()
    {
        _startedGame?.Unsuscribe(HandlePlayedEvent);
    }

    private void Awake()
    {
        StartCoroutine(Initiate());
    }

    private IEnumerator Initiate()
    {
        SceneManager.LoadSceneAsync(_menusIndex, LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        _menuNameEvent?.InvokeEvent(_mainMenuName);
    }

    private void HandlePlayedEvent(bool isPlaying)
    {
        if (isPlaying)
        {
            StartCoroutine(LoadGame());
            
        }
        else
        {
            StartCoroutine(UnloadGame());
        }
    }

    private IEnumerator LoadGame()
    {
        _menuNameEvent?.InvokeEvent(_loadingUI);
        var temp = SceneManager.LoadSceneAsync(_level1Index, LoadSceneMode.Additive);
        yield return new WaitUntil(() => temp.isDone);
        _menuNameEvent?.InvokeEvent("Play");
    }

    private IEnumerator UnloadGame()
    {
        _menuNameEvent?.InvokeEvent(_loadingUI);
        var temp = SceneManager.UnloadSceneAsync(_level1Index);
        yield return new WaitUntil(() => temp.isDone);
        _menuNameEvent?.InvokeEvent("MainMenu");
    }
}