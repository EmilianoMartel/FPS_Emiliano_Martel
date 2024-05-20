using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected HealthPoints p_healthPoints;
    [SerializeField] protected float p_waitForDie;

    public event Action<bool> onDie = delegate { };


    protected virtual void OnEnable()
    {
        p_healthPoints.dead += HandleDie;
    }

    protected virtual void OnDisable()
    {
        p_healthPoints.dead -= HandleDie;
    }

    protected virtual void Awake()
    {
        if (!p_healthPoints)
        {
            Debug.LogError($"{name}: HealthPoints is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    protected virtual void HandleDie()
    {
        StartCoroutine(DieLogic());
    }

    private IEnumerator DieLogic()
    {
        onDie?.Invoke(true);
        yield return new WaitForSeconds(p_waitForDie);
        gameObject.SetActive(false);
        onDie?.Invoke(false);
    }
}