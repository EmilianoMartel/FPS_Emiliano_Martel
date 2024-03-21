using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Character : MonoBehaviour
{
    [SerializeField] private HealthPoints _healthPoints;
    [SerializeField] private float _waitForDie;

    private void OnEnable()
    {
        _healthPoints.dead += HandleDie;
    }

    private void OnDisable()
    {
        _healthPoints.dead -= HandleDie;
    }

    private void Awake()
    {
        if (!_healthPoints)
        {
            Debug.LogError($"{name}: HealthPoints is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleDie()
    {
        StartCoroutine(DieLogic());
    }

    private IEnumerator DieLogic()
    {
        yield return new WaitForSeconds(_waitForDie);
        gameObject.SetActive(false);
    }
}
