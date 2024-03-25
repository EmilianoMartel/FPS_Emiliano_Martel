using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private HealthPoints _healthPoints;
    [SerializeField] private ParticlesEffect _particlesEffect;
    [Header("Parameters")]
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _powerExplotion = 10f;

    private void OnEnable()
    {
        _healthPoints.dead += HandleExplotionOnDead;   
    }

    private void OnDisable()
    {
        _healthPoints.dead -= HandleExplotionOnDead;
    }

    private void Awake()
    {
        if (!_healthPoints)
        {
            Debug.LogError($"{name}: Health points is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;

        Gizmos.DrawSphere(transform.position, _radius);
    }

    private void HandleExplotionOnDead()
    {
        _particlesEffect.ActiveParticles();
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rib))
            {
                rib.AddExplosionForce(_powerExplotion, explosionPos, _radius, 3.0F);
            }
        }

    }
}