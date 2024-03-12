using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Parameters")]
    [Tooltip("Gun damage")]
    [SerializeField] private int _damage;
    [Tooltip("Max shoot distance.")]
    [SerializeField] private int _shootDistance;
    [Tooltip("What layers are enemies.")]
    [SerializeField] private LayerMask _enemyMask;
    [Tooltip("The shoot start point.")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private FirstPersonController _firstPersonController;
    [Tooltip("Set if the gun is automatic shoot.")]
    [SerializeField] private bool _isAutomatic;
    [Tooltip("Time between shoots in RPM(round per minute).")]
    [SerializeField] private float _rateOfFire;

    private bool _isShooting;

    private void OnEnable()
    {
        _firstPersonController.shootEvent += CanShoot;
    }

    private void OnDisable()
    {
        _firstPersonController.shootEvent -= CanShoot;
    }

    private void Awake()
    {
        if (!_firstPersonController)
        {
            Debug.LogError($"{name}: FirstPersonController is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (_enemyMask.value == 0)
        {
            Debug.LogError($"{name}: Select a LayerMask.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (_isShooting)
        {
            Shoot();
        }
    }

    private void CanShoot(bool isShooting)
    {
        _isShooting = isShooting;
    }

    private void Shoot()
    {
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_shootPoint.position, _shootPoint.forward, out hit, _shootDistance, _enemyMask))
        {
            HealthPoints healthPoints = hit.transform.GetComponentInParent<HealthPoints>();
            healthPoints.TakeDamage(_damage);


            Debug.DrawRay(_shootPoint.position, _shootPoint.position + new Vector3(0, 0, 1000), Color.yellow, 2);
        }
        else
        {
            Debug.DrawRay(_shootPoint.position, _shootPoint.position + new Vector3(0, 0, 1000), Color.white, 2);
        }
    }
}
