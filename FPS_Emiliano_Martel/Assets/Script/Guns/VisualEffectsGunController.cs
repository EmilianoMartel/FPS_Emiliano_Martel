using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsGunController : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private ParticleSystem _spark;
    [SerializeField] private ParticleSystem _flash;

    private void OnEnable()
    {
        _gun.shootMoment += HandleStartExplotion;
    }

    private void OnDisable()
    {
        _gun.shootMoment -= HandleStartExplotion;
    }

    private void Awake()
    {
        if (!_gun)
        {
            Debug.LogError($"{name}: Gun is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_spark)
        {
            Debug.LogError($"{name}: Spark is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!_flash)
        {
            Debug.LogError($"{name}: Flash is null.\nCheck and assigned one.\nDisabled component.");
            enabled = false;
            return;
        }
    }

    private void HandleStartExplotion()
    {
        _spark.Play();
        _flash.Play();
    }
}
