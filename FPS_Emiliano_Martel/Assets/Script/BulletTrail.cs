using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class BulletTrail : MonoBehaviour
{
    [SerializeField] private ParticleSystem _impactSystem;
    [SerializeField] private IObjectPool<TrailRenderer> _trailPool;

    private Rigidbody Rigidbody;
    public delegate void OnDisableCallback(BulletTrail Instance);
    public OnDisableCallback Disable;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 Position, Vector3 Direction, float Speed)
    {
        Rigidbody.velocity = Vector3.zero;
        transform.position = Position;
        transform.forward = Direction;

        Rigidbody.AddForce(Direction * Speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        _impactSystem.transform.forward = -1 * transform.forward;
        _impactSystem.Play();
        Rigidbody.velocity = Vector3.zero;
    }

    private void OnParticleSystemStopped()
    {
        Disable?.Invoke(this);
    }
}
