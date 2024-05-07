using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonGenerator : MonoBehaviour
{
    [SerializeField] private List<string> _buttonsName = new();
    [SerializeField] private StringChannel _menuNameEvent;
    [SerializeField] private ButtonLogic _buttonPrefab;

    private List<ButtonLogic> _logic = new();

    private void Awake()
    {
        for (int i = 0; i < _buttonsName.Count; i++)
        {
            var temp = Instantiate(_buttonPrefab, transform);
            temp.transform.parent = transform;
            temp.menuNameEvent = _menuNameEvent;
            temp.nameMenu = _buttonsName[i];
            temp.SetButton();
            _logic.Add(temp);
        }
    }
}