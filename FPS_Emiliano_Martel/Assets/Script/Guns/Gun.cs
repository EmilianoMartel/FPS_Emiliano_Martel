using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : Weapon
{
    [Header("Gun Parameters")]
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

    [Header("Optional parameters")]
    [SerializeField] private RecoilSO _recoilData;
    [SerializeField] private GunSlot _gunSlot;

    private bool _isPressTrigger;
    private bool _isShooting = false;
    private bool _canShoot = true;
    private bool _isReloaded;

    private float _timeBetweenShoot;
    private int _ammoLeft;

    [Header("Channels")]
    [SerializeField] private EmptyAction _shootEvent;
    public Action<bool> viewEnemy = delegate { };
    [SerializeField] private ActionChanel<int> _actualAmmoEvent;
    [SerializeField] private ActionChanel<int> _maxAmmoEvent;
    [SerializeField] private ActionChanel<Transform> _pointShootEvent;
    [SerializeField] private ActionChanel<int> _damageValueEvent;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (_maxAmmoEvent)
            _maxAmmoEvent.InvokeEvent(_maxAmmo);

        if (_pointShootEvent)
            _pointShootEvent.InvokeEvent(_shootPoint);

        _firstPersonController.shootEvent += HandleSetPressTrigger;
        _firstPersonController.reloadEvent += HandleReload;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _firstPersonController.shootEvent -= HandleSetPressTrigger;
        _firstPersonController.reloadEvent -= HandleReload;
    }

    private void Awake()
    {
        NullReferenceController();

        //This count is to have the time between shots.
        _timeBetweenShoot = 60 / _fireRate;

        _ammoLeft = _maxAmmo;
    }

    private void Start()
    {
        if(_maxAmmoEvent)
            _maxAmmoEvent.InvokeEvent(_maxAmmo);

        if(_actualAmmoEvent)
            _actualAmmoEvent.InvokeEvent(_ammoLeft);
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

    protected override void HandleSetPressTrigger(bool pressTrigger)
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

        if (_shootEvent)
            _shootEvent.InvokeEvent();

        _ammoLeft--;
        if (_actualAmmoEvent)
            _actualAmmoEvent.InvokeEvent(_ammoLeft);

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
        if(_actualAmmoEvent)
            _actualAmmoEvent.InvokeEvent(_ammoLeft);

        _isReloaded = false;
    }

    public override void SendWeaponParameters()
    {
        if(_actualAmmoEvent)
            _actualAmmoEvent.InvokeEvent(_ammoLeft);
        if(_maxAmmoEvent)
            _maxAmmoEvent.InvokeEvent(_maxAmmo);
        if(_pointShootEvent)
            _pointShootEvent.InvokeEvent(_shootPoint);
        if (_damageValueEvent)
            _damageValueEvent.InvokeEvent(p_damage);
    }

    private void NullReferenceController()
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
    }
}