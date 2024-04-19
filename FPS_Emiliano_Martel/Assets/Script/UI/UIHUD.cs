using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Image _crossHair;
    [SerializeField] private TMP_Text _ammoText;

    [Header("CrossView")]
    [SerializeField] private Color _viewEnemyColor;
    [SerializeField] private Color _dontViewEnemyColor;

    [Header("Observers and Guns")]
    [SerializeField] private Gun _gun;

    [Header("Channels")]
    [SerializeField] private ActionChanel<int> _actualAmmoEvent;
    [SerializeField] private ActionChanel<int> _maxAmmoEvent;

    private int _maxAmmo = 0;

    private void OnEnable()
    {
        if (_actualAmmoEvent)
            _actualAmmoEvent.Sucription(HandleChangeAmmo);
        if (_maxAmmoEvent)
            _maxAmmoEvent.Sucription(HandleMaxAmmo);

        _gun.viewEnemy += HandleLookEnemy;
    }

    private void OnDisable()
    {
        if (_actualAmmoEvent)
            _actualAmmoEvent.Unsuscribe(HandleChangeAmmo);
        if (_maxAmmoEvent)
            _maxAmmoEvent.Unsuscribe(HandleMaxAmmo);

        _gun.viewEnemy += HandleLookEnemy;
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