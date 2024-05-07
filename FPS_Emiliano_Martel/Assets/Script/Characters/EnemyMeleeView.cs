using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeView : EnemyView
{
    [SerializeField] private string _isAttacking = "isAttacking";

    private EnemyMeleeAttack _meele;

    protected override void OnEnable()
    {
        base.OnEnable();
        _meele.onAttack += HandleAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _meele.onAttack -= HandleAttack;
    }

    protected override void Awake()
    {
        base.Awake();

        _meele = p_enemy as EnemyMeleeAttack;
    }

    private void HandleAttack(bool attack)
    {
        p_animator.SetBool(_isAttacking,attack);
    }
}