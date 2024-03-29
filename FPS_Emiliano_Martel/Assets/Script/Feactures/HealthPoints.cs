using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [Tooltip("Life points must be greater than 0.")]
    [SerializeField] private int _maxLifePoints;

    private int _actualLife;

    public Action<float> damagedEvent;
    public Action dead;

    private void Awake()
    {
        if (_maxLifePoints <= 0)
        {
            Debug.LogError($"{name}: Life points cannot be 0 or less.\nCheck and assigned another number.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        _actualLife = _maxLifePoints;
    }

    public void TakeDamage(int damage)
    {
        _actualLife -= damage;
        Debug.Log($"{name} was damaged, life: {_actualLife}");
        if (_actualLife <= 0)
        {
            Dead();
            return;
        }
        damagedEvent?.Invoke((float)_actualLife / (float)_maxLifePoints);

    }

    [ContextMenu("Take 1 point of damage")]
    private void BasicDamage()
    {
        _actualLife--;
        damagedEvent?.Invoke(_actualLife / _maxLifePoints);
        if (_actualLife <= 0)
        {
            Dead();
        }
    }

    [ContextMenu("Take total damage")]
    private void TakeTotalDamage()
    {
        TakeDamage(_actualLife);
    }

    private void Dead()
    {
        dead?.Invoke();
        _actualLife = _maxLifePoints;
    }
}
