using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] protected NavMeshAgent p_agent;
    [SerializeField] protected Transform p_targetGenerator;
    [SerializeField] protected Canvas p_lifeView;

    [Header("Parameters")]
    [SerializeField] protected float p_speed = 3;

    protected bool p_canMoveToGenerator = true;

    public Transform target { set { p_targetGenerator = value; } }

    protected override void Awake()
    {
        base.Awake();
        p_agent.speed = p_speed;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (p_canMoveToGenerator)
            p_agent.SetDestination(p_targetGenerator.transform.position);
        
    }

    protected override void HandleDie()
    {
        p_agent.speed = 0;
        p_lifeView.gameObject.SetActive(false);
        base.HandleDie();
    }
}