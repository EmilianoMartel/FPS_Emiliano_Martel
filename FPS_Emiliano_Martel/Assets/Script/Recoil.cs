using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Recoil : MonoBehaviour
{
    [Header("Recoil Parameters")]
    [SerializeField] private float _recoilInX = 1f;
    [SerializeField] private float _recoilInY = 1f;
    [Header("Settings")]
    [SerializeField] private float _snappiness = 1f;
    [SerializeField] private float _returnSpeed = 1f;
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
    private void Update()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    private void HandleShootMoment()
    {
        _targetRotation += new Vector3(_recoilInX,_recoilInY,0);
    }
}
