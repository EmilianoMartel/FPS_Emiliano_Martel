using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : Character
{
    [Header("Parameters")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeTime = 10f;
    [Header("Visual parameters")]
    [SerializeField] private ParticleSystem _impactSystem;
    [SerializeField] private IObjectPool<TrailRenderer> _trailPool;

    private Rigidbody _rigidbody;
    public Action<Bullet> onDisable;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onDisable?.Invoke(this);
    }

    protected override void Awake()
    {
        base.Awake();
        if (gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            _rigidbody = rb;
        }
        else
        {
            Debug.LogError($"{name}: Rigidbody is null.\nCheck and assigned component.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    public void Shoot(Vector3 Position, Vector3 Direction, float Speed)
    {
        ActiveBullet();
        _rigidbody.velocity = Vector3.zero;
        transform.position = Position;
        transform.forward = Direction;

        _rigidbody.AddForce(Direction * Speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        _impactSystem.transform.forward = -1 * transform.forward;
        _impactSystem.Play();
        _rigidbody.velocity = Vector3.zero;
        HandleDie();
    }

    protected override void HandleDie()
    {
        _rigidbody.AddForce(Vector3.zero,ForceMode.Force);
        gameObject.SetActive(false);
    }

    private void ActiveBullet()
    {
        gameObject.SetActive(true);
        StartCoroutine(WaitForDieLogic());
    }

    private IEnumerator WaitForDieLogic()
    {
        yield return new WaitForSeconds(_lifeTime);
        HandleDie();
    }
}