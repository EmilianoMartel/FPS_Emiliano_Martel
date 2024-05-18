using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] protected NavMeshAgent p_agent;
    [SerializeField] protected Transform p_target;
    [SerializeField] protected Canvas p_lifeView;
    [SerializeField] private BoolChanelSo _startedGame;

    [Header("Parameters")]
    [SerializeField] protected float p_speed = 3;

    protected HealthPoints p_targetHp;
    protected bool p_canMoveToGenerator = true;
    protected bool p_isPlaying = true;

    public Transform target { set { p_target = value; } }

    protected override void OnEnable()
    {
        base.OnEnable();
        _startedGame?.Sucription(HanldePause);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _startedGame?.Unsuscribe(HanldePause);
    }

    protected override void Awake()
    {
        base.Awake();
        p_agent.speed = p_speed;
    }

    protected virtual void Update()
    {
        if (!p_isPlaying)
            return;

        Move();
    }

    protected virtual void Move()
    {
        if (p_canMoveToGenerator)
            p_agent.SetDestination(p_target.transform.position);
        
    }

    protected override void HandleDie()
    {
        p_agent.speed = 0;
        p_lifeView.gameObject.SetActive(false);
        base.HandleDie();
    }

    public void SetTarget(Transform target)
    {
        p_target = target;
        if (p_target.TryGetComponent<HealthPoints>(out HealthPoints hp))
            p_targetHp = hp;
    }

    private void HanldePause(bool isPlaying)
    {
        p_isPlaying = isPlaying;
    }
}