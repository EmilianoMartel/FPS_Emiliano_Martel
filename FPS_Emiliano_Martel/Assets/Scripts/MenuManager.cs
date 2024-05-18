using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<ActiveMenu> _menues = new();

    [Header("Channels")]
    [SerializeField] private StringChannel _menuNameEvent;

    private void OnEnable()
    {
        _menuNameEvent?.Sucription(HandleMenu);
    }

    private void OnDisable()
    {
        _menuNameEvent?.Unsuscribe(HandleMenu);
    }

    private void HandleMenu(string name)
    {
        if (name == "Exit")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        foreach (var menu in _menues)
        {
            if (menu.menuName == name)
            {
                menu.HandleActiveMenu(true);
                continue;
            }
            menu.HandleActiveMenu(false);
        }
    }

    private IEnumerator ActiveMenues(string name)
    {
        foreach (var menu in _menues)
        {
            yield return new  WaitForSeconds(0.1f);
            if (menu.menuName == name)
            {
                menu.HandleActiveMenu(true);
                continue;
            }
            menu.HandleActiveMenu(false);
        }
    }
}