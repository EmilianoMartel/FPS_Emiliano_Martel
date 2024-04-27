using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : Enemy
{
    [SerializeField] private float _attackDistance = 2;
    [SerializeField] private float _waitForAttack = 1.5f;
    [SerializeField] private int _damage = 1;
    private HealthPoints _hp;

    private bool _isAttacking = false;

    public event Action onAttack = delegate { };

    protected override void Awake()
    {
        base.Awake();
        if(p_target.TryGetComponent<HealthPoints>(out HealthPoints hp))
            _hp = hp;
    }

    protected override void Update()
    {
        base.Update();
        if ((transform.position - p_target.position).magnitude <= _attackDistance && !_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private void AttackPlayer()
    {
        if (_hp)
            _hp.TakeDamage(_damage);
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(_waitForAttack);
        AttackPlayer();
        _isAttacking = false;
    }
}