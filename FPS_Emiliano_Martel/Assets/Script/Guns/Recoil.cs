using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Recoil : MonoBehaviour
{
    [Header("Recoil Parameters")]
    [SerializeField] private RecoilSO _recoilData;
    [Header("Managers")]
    [SerializeField] private Gun _gun;

    [SerializeField] private float _threshold = 0.0001f;
    //Rotations
    private Quaternion _currentRotation;
    private Quaternion _targetRotation;

    private void OnEnable()
    {
        _gun.shootMoment += HandleShootMoment;
    }

    private void OnDisable()
    {
        _gun.shootMoment += HandleShootMoment;
    }

    private void Awake()
    {
        if (!_recoilData)
        {
            Debug.LogError($"{name}: Data is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (Quaternion.Dot(_targetRotation, _targetRotation) < _threshold * _threshold)
        {
            _targetRotation = Quaternion.Euler(Vector3.one * _threshold);
            return;
        }
        _targetRotation = Quaternion.Lerp(_targetRotation, Quaternion.identity, _recoilData.returnSpeed * Time.deltaTime);
        _currentRotation = Quaternion.Slerp(_currentRotation, _targetRotation, _recoilData.snappiness * Time.fixedDeltaTime);
        transform.localRotation = _currentRotation;
    }

    private void HandleShootMoment()
    {
        Vector3 rotation = Vector3.zero;
        rotation.x = _recoilData.recoilX;
        rotation.y = Random.Range(_recoilData.minRecoilY, _recoilData.maxRecoilY);
        rotation.z = Random.Range(_recoilData.minRecoilZ, _recoilData.maxRecoilZ);
        _targetRotation *= Quaternion.Euler(rotation);
    }
}