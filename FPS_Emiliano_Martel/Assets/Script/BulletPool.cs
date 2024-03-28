using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private BulletTrail _bulletPrefab;
    [SerializeField] private Transform _pointShoot;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool UseObjectPool = false;

    private ObjectPool<BulletTrail> _bulletPool;

    private void OnEnable()
    {
        _gun.shootMoment += HandleShoot;
    }

    private void OnDisable()
    {
        _gun.shootMoment -= HandleShoot;
    }

    private void Awake()
    {
        _bulletPool = new ObjectPool<BulletTrail>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 200, 100_000);
    }

    private BulletTrail CreatePooledObject()
    {
        BulletTrail instance = Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity);
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(BulletTrail Instance)
    {
        _bulletPool.Release(Instance);
    }

    private void OnTakeFromPool(BulletTrail Instance)
    {
        Instance.gameObject.SetActive(true);
        SpawnBullet(Instance);
        Instance.transform.SetParent(transform, true);
    }

    private void OnReturnToPool(BulletTrail Instance)
    {
        Instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(BulletTrail Instance)
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

    private void SpawnBullet(BulletTrail Instance)
    {
        Instance.transform.position = _pointShoot.transform.position;

        Instance.Shoot(Instance.transform.position, _pointShoot.transform.forward, Speed);
    }
}
