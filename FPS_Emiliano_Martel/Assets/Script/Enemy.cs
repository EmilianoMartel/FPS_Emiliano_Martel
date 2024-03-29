using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;

    public Transform target { set { _target = value; } }

    private void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }
}