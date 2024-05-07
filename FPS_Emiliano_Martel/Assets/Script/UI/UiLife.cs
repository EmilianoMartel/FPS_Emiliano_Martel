using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLife : MonoBehaviour
{
    [SerializeField] private Image _maxLife;
    [SerializeField] private ActionChanel<float> _health;

    private void OnEnable()
    {
        _health?.Sucription(HandleChangeLife);
    }

    private void OnDisable()
    {
        _health?.Unsuscribe(HandleChangeLife);
    }

    private void Awake()
    {
        if (_maxLife == null)
        {
            Debug.LogError($"{name}: MaxLife is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleChangeLife(float life)
    {
        _maxLife.fillAmount = life;
    }
}
