using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : Enemy
{
    [SerializeField] private float _attackDistance = 2;
    [SerializeField] private float _waitForAttack = 1.5f;
    [SerializeField] private int _damage = 1;
   

    private bool _isAttacking = false;
    private bool _canMoveToPlayer = true;

    public event Action<bool> onAttack = delegate { };

    protected override void Awake()
    {
        base.Awake();

        p_canMoveToGenerator = false;
    }

    protected override void Update()
    {
        base.Update();
        if ((transform.position - p_target.position).magnitude <= _attackDistance && !_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    protected override void Move()
    {
        if (_canMoveToPlayer)
            p_agent.SetDestination(p_target.transform.position);
    }

    private void AttackPlayer()
    {
        if (p_targetHp)
            p_targetHp.TakeDamage(_damage);
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        onAttack?.Invoke(true);
        yield return new WaitForSeconds(_waitForAttack);
        AttackPlayer();
        _isAttacking = false;
        onAttack?.Invoke(false);
    }
}