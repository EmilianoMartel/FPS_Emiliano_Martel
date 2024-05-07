using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] private EmptyAction _playEvent;
    [SerializeField] private StringChannel _menuNameEvent;
    [SerializeField] private string _mainMenuName = "MainMenu";
    [SerializeField] private int _level1Index = 2;
    [SerializeField] private int _menusIndex = 1;

    private void OnEnable()
    {
        _playEvent?.Sucription(HandlePlayeEvent);   
    }

    private void OnDisable()
    {
        _playEvent?.Unsuscribe(HandlePlayeEvent);
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

    private void HandlePlayeEvent()
    {
        SceneManager.LoadSceneAsync(_level1Index,LoadSceneMode.Additive);
    }
}