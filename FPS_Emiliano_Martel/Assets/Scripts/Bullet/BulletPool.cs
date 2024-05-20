using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _pointShoot;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private bool _debugEnable = false;

    [SerializeField] private EmptyAction _shootMomentEvent;
    [SerializeField] private ActionChanel<Transform> _pointShootEvent;

    private List<Bullet> _activeBullets = new();
    private List<Bullet> _poolBullets = new();

    private void OnEnable()
    {
        if (_shootMomentEvent)
            _shootMomentEvent.Sucription(HandleShoot);

        if (_pointShootEvent)
            _pointShootEvent.Sucription(HandleSetShootPoint);
    }

    private void OnDisable()
    {
        _shootMomentEvent.Unsuscribe(HandleShoot);

        if (_pointShootEvent)
            _pointShootEvent.Unsuscribe(HandleSetShootPoint);

        _activeBullets.Clear();
        _poolBullets.Clear();
    }

    private void OnGUI()
    {
        if (_debugEnable)
        {
            GUI.Label(new Rect(10, 10, 200, 30), $"Total Pool Size: {_activeBullets.Count + _poolBullets.Count}");
            GUI.Label(new Rect(10, 30, 200, 30), $"Active Objects: {_activeBullets.Count}");
        }
    }

    private void HandleSetShootPoint(Transform pointShoot)
    {
        _pointShoot = pointShoot;
    }

    private void HandleShoot()
    {
        Bullet temp = SelectBullet();
        temp.transform.position = _pointShoot.position;

        if (_poolBullets.Contains(temp))
            _poolBullets.Remove(temp);

        _activeBullets.Add(temp);
        temp.Shoot(_pointShoot.position, _pointShoot.forward, _speed);
    }

    private Bullet SelectBullet()
    {
        if (_poolBullets.Count == 0)
        {
            Bullet temp = Instantiate(_bulletPrefab, _pointShoot.position, Quaternion.identity);
            temp.onDisable += HandleDesactiveBullet;
            temp.transform.parent = transform;
            return temp;
        }
        _poolBullets[0].onDisable += HandleDesactiveBullet;
        return _poolBullets[0];
    }

    private void HandleDesactiveBullet(Bullet bullet)
    {
        bullet.onDisable -= HandleDesactiveBullet;
        if (_activeBullets.Contains(bullet))
        {
            _activeBullets.Remove(bullet);
            _poolBullets.Add(bullet);
        }
    }
}