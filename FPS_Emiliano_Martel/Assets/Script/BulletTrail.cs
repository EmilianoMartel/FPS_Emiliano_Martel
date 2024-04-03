using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class BulletTrail : Character
{
    [SerializeField] private ParticleSystem _impactSystem;
    [SerializeField] private IObjectPool<TrailRenderer> _trailPool;

    private Rigidbody _rigidbody;
    public delegate void OnDisableCallback(BulletTrail Instance);
    public OnDisableCallback Disable;

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
    }

    private void OnParticleSystemStopped()
    {
        Disable?.Invoke(this);
    }

    protected override void HandleDie()
    {
        base.HandleDie();
        _rigidbody.AddForce(Vector3.zero,ForceMode.Force);
    }
}
