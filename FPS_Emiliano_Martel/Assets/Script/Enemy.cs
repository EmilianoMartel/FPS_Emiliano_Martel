using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] protected Transform p_target;

    public Transform target { set { p_target = value; } }

    protected virtual void Update()
    {
        _agent.SetDestination(p_target.transform.position);
    }
}