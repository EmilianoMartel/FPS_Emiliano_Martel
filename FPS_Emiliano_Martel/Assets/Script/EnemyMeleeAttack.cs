using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : Enemy
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _attackDistance = 2;
    [SerializeField] private float _waitForAttack = 1.5f;
    [SerializeField] private int _damage = 1;
    private HealthPoints _hp;

    private bool _isAttacking = false;
    private bool _canMoveToPlayer = true;

    public Transform player { set { _player = value; } }

    public event Action<bool> onAttack = delegate { };

    protected override void Awake()
    {
        base.Awake();
        if(_player.TryGetComponent<HealthPoints>(out HealthPoints hp))
            _hp = hp;
        p_canMoveToGenerator = false;
    }

    protected override void Update()
    {
        base.Update();
        if ((transform.position - _player.position).magnitude <= _attackDistance && !_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    protected override void Move()
    {
        base.Move();
        if (_canMoveToPlayer)
            p_agent.SetDestination(_player.transform.position);
    }

    private void AttackPlayer()
    {
        if (_hp)
            _hp.TakeDamage(_damage);
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