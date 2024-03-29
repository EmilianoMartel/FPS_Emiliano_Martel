using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLife : MonoBehaviour
{
    [SerializeField] private Image _maxLife;
    [SerializeField] private HealthPoints _healthPoints;

    private void OnEnable()
    {
        _healthPoints.damagedEvent += HandleChangeLife;
    }

    private void OnDisable()
    {
        _healthPoints.damagedEvent -= HandleChangeLife;
    }

    private void Awake()
    {
        if (_healthPoints == null)
        {
            Debug.LogError($"{name}: HealthPoints is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
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
