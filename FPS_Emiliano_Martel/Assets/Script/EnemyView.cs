using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] protected Animator p_animator;
    [SerializeField] protected Enemy p_enemy;

    [Header("Animator parameters")]
    [SerializeField] private string _isDie = "isDie";

    protected virtual void OnEnable()
    {
        p_enemy.onDie += HandleDie;
    }

    protected virtual void OnDisable()
    {
        p_enemy.onDie -= HandleDie;
    }

    protected virtual void Awake()
    {
        NullReferenceController();
    }

    private void HandleDie(bool isDie)
    {
        p_animator.SetBool(_isDie,isDie);
    }

    private void NullReferenceController()
    {
        if (!p_animator)
        {
            Debug.LogError($"{name}: Animator is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}
