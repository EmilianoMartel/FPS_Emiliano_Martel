using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMenu : MonoBehaviour
{
    [SerializeField] private string _menuName = "Menu";
    [SerializeField] private Canvas _mainCanvas;

    public string menuName { get { return _menuName; } }

    public void HandleActiveMenu(bool isActive)
    {
        _mainCanvas.gameObject.SetActive(isActive);
    }
}