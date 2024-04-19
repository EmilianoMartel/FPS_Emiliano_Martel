using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _pointShoot;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool UseObjectPool = false;

    [SerializeField] private EmptyAction _shootMomentEvent;
    [SerializeField] private ActionChanel<Transform> _pointShootEvent;

    private ObjectPool<Bullet> _bulletPool;

    private void OnEnable()
    {
        if(_shootMomentEvent)
            _shootMomentEvent.Sucription(HandleShoot);

        if (_pointShootEvent)
            _pointShootEvent.Sucription(HandleNewPointShoot);
    }

    private void OnDisable()
    {
        if (_shootMomentEvent)
            _shootMomentEvent.Unsuscribe(HandleShoot);

        if (_pointShootEvent)
            _pointShootEvent.Unsuscribe(HandleNewPointShoot);
    }

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 200, 100_000);
    }

    private Bullet CreatePooledObject()
    {
        Bullet instance = Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(Bullet Instance)
    {
        _bulletPool.Release(Instance);
    }

    private void OnTakeFromPool(Bullet Instance)
    {
        Instance.gameObject.SetActive(true);
        SpawnBullet(Instance);
        Instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(Bullet Instance)
    {
        Instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Bullet Instance)
    {
        Destroy(Instance.gameObject);
    }

    private void OnGUI()
    {
        if (UseObjectPool)
        {
            GUI.Label(new Rect(10, 10, 200, 30), $"Total Pool Size: {_bulletPool.CountAll}");
            GUI.Label(new Rect(10, 30, 200, 30), $"Active Objects: {_bulletPool.CountActive}");
        }
    }

    private void HandleShoot()
    {
        _bulletPool.Get();
    }

    private void SpawnBullet(Bullet Instance)
    {
        Instance.transform.position = _pointShoot.transform.position;

        Instance.Shoot(Instance.transform.position, _pointShoot.transform.forward, Speed);
    }

    private void HandleNewPointShoot(Transform pointShoot)
    {
        _pointShoot = pointShoot;
    }
}