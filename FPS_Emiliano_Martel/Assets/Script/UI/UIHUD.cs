using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private RawImage _crossHair;
    [SerializeField] private TMP_Text _ammoText;

    [Header("CrossView")]
    [SerializeField] private Color _viewEnemyColor;
    [SerializeField] private Color _dontViewEnemyColor;

    [SerializeField] private Gun _gun;

    private int _maxAmmo = 0;

    private void OnEnable()
    {
        _gun.actualAmmo += HandleChangeAmmo;
        _gun.maxAmmo += HandleMaxAmmo;
        _gun.viewEnemy += HandleLookEnemy;
    }

    private void OnDisable()
    {
        _gun.actualAmmo -= HandleChangeAmmo;
        _gun.maxAmmo -= HandleMaxAmmo;
        _gun.viewEnemy -= HandleLookEnemy;
    }

    private void Awake()
    {
        if (!_gun)
        {
            Debug.LogError($"{name}: Gun is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (_crossHair == null)
        {
            Debug.LogError($"{name}: CrossHair is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_ammoText)
        {
            Debug.LogError($"{name}: AmmoText is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleLookEnemy(bool look)
    {
        if (look)
        {
            _crossHair.color = _viewEnemyColor;
        }
        else
        {
            _crossHair.color = _dontViewEnemyColor;
        }
    }

    private void HandleChangeAmmo(int actualAmmo)
    {
        _ammoText.text = $"{actualAmmo} / {_maxAmmo}";
    }

    private void HandleMaxAmmo(int maxAmmo)
    {
        _maxAmmo = maxAmmo;
    }
}