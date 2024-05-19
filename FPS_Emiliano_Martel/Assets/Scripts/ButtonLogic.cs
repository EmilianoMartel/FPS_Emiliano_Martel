using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private string _playName = "Play";
    [SerializeField] private BoolChanelSo _startedGame;

    private string _nameMenu;
    private StringChannel _menuNameEvent;
    public string nameMenu { set { _nameMenu = value; } }
    public StringChannel menuNameEvent { set { _menuNameEvent = value; } }

    private void Awake()
    {
        _button.onClick.AddListener(InvokeEvent);
    }

    public void SetButton()
    {
        _text.text = _nameMenu;
        if (_nameMenu == "Resume")
            _nameMenu = "Play";
        if(_nameMenu == "Back to Menu")
            _nameMenu = "Menu";
    }

    private void InvokeEvent()
    {
        _menuNameEvent?.InvokeEvent(_nameMenu);
        if (_nameMenu == _playName)
            _startedGame?.InvokeEvent(true);
        else if(_text.text == "Back to Menu")
        {
            _startedGame?.InvokeEvent(false);
            Time.timeScale = 1;
        }
    }
}
