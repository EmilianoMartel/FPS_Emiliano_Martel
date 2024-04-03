using System;
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
    [SerializeField] private float _fireRate;
    [Tooltip("Total ammo.")]
    [SerializeField] private int _maxAmmo;
    [Tooltip("The time it takes to reload the gun.")]
    [SerializeField] private float _timeReload;
    [SerializeField] private RecoilSO _recoilData;

    private bool _isPressTrigger;
    private bool _isShooting = false;
    private bool _canShoot = true;
    private bool _isReloaded;

    private float _timeBetweenShoot;
    private int _ammoLeft;

    public Action shootMoment = delegate { };
    public Action<bool> viewEnemy = delegate { };
    public Action<int> actualAmmo = delegate { };
    public Action<int> maxAmmo = delegate { };

    private void OnEnable()
    {
        _firstPersonController.shootEvent += HandleSetPressTrigger;
        _firstPersonController.reloadEvent += HandleReload;
    }

    private void OnDisable()
    {
        _firstPersonController.shootEvent -= HandleSetPressTrigger;
        _firstPersonController.reloadEvent -= HandleReload;
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
        if (_fireRate <= 0)
        {
            Debug.LogError($"{name}: Rate fire cannot be 0 or less.\nCheck and assigned a valid number.\nDisabled component.");
            enabled = false;
            return;
        }
        if (_maxAmmo <= 0)
        {
            Debug.LogError($"{name}: Max Ammo cannot be 0 or less.\nCheck and assigned a valid number.\nDisabled component.");
            enabled = false;
            return;
        }
        if (_timeReload <= 0)
        {
            Debug.LogError($"{name}: TimeReload cannot be 0 or less.\nCheck and assigned a valid number.\nDisabled component.");
            enabled = false;
            return;
        }

        //This count is to have the time between shots.
        _timeBetweenShoot = 60 / _fireRate;

        _ammoLeft = _maxAmmo;
    }

    private void Start()
    {
        maxAmmo?.Invoke(_maxAmmo);
        actualAmmo?.Invoke(_ammoLeft);
    }

    private void Update()
    {
        if (_isPressTrigger && !_isShooting && _canShoot && _ammoLeft > 0 && !_isReloaded)
        {
            StartCoroutine(Shoot());
        }

        RaycastHit hit;

        viewEnemy?.Invoke(Physics.Raycast(_shootPoint.position, _shootPoint.forward, out hit, _shootDistance, _enemyMask));
    }

    private void HandleSetPressTrigger(bool pressTrigger)
    {
        _isPressTrigger = pressTrigger;
        if (!_isAutomatic && !pressTrigger)
        {
            _canShoot = true;
        }
    }

    private IEnumerator Shoot()
    {
        _isShooting = true;
        _canShoot = false;

        shootMoment?.Invoke();

        _ammoLeft--;

        actualAmmo?.Invoke(_ammoLeft);

        yield return new WaitForSeconds(_timeBetweenShoot);

        if (_isAutomatic)
        {
            _canShoot = true;
        }
        else
        {
            _canShoot = false;
        }

        _isShooting = false;
    }

    private void HandleReload()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _isReloaded = true;

        yield return new WaitForSeconds(_timeReload);

        _ammoLeft = _maxAmmo;
        actualAmmo?.Invoke(_ammoLeft);
        _isReloaded = false;
    }
}