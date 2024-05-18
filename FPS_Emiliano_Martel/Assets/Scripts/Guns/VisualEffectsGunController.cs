using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsGunController : MonoBehaviour
{
    [Header("EventChannels")]
    [SerializeField] private EmptyAction _shootMoment;
    [Header("Particles")]
    [SerializeField] private ParticleSystem _spark;
    [SerializeField] private ParticleSystem _flash;

    private void OnEnable()
    {
        if(_shootMoment)
            _shootMoment.Sucription(HandleStartExplotion);
        _spark.gameObject.SetActive(true);
        _flash.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if(_shootMoment)
            _shootMoment.Unsuscribe(HandleStartExplotion);
        _spark.gameObject.SetActive(false);
        _flash.gameObject.SetActive(false);
    }

    private void Awake()
    {
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
