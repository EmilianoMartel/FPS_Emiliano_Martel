using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Guns Data", menuName = "RecoilData")]
public class RecoilSO : ScriptableObject
{
    [Header("Recoil Parameters")]
    [Tooltip("X recoil is the vertical movement. Negative go down.")]
    [SerializeField] private float _recoilX = 1f;
    [SerializeField] private float _minRecoilY = -1f;
    [Tooltip("X recoil is the horizontal movement.")]
    [SerializeField] private float _maxRecoilY = 1f;
    [SerializeField] private float _minRecoilZ = -1f;
    [SerializeField] private float _maxRecoilZ = 1f;
    [Header("Settings")]
    [Tooltip("")]
    [SerializeField] private float _snappiness = 1f;
    [SerializeField] private float _returnSpeed = 1f;
    
    public float recoilX { get { return _recoilX; } }
    public float minRecoilY { get { return _minRecoilY; } }
    public float maxRecoilY { get { return _maxRecoilY; } }
    public float minRecoilZ { get { return _minRecoilZ; } }
    public float maxRecoilZ { get { return _maxRecoilZ; } }
    public float snappiness { get { return _snappiness; } }
    public float returnSpeed { get { return _returnSpeed; } }
}
