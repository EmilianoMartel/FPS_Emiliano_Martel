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

    //Rotations
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

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
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _recoilData.returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _recoilData.snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    private void HandleShootMoment()
    {
        _targetRotation += new Vector3(_recoilData.recoilX, Random.Range(_recoilData.minRecoilY, _recoilData.maxRecoilY), Random.Range(_recoilData.minRecoilZ, _recoilData.maxRecoilZ));
    }
}