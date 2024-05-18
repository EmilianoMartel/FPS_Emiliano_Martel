using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _effect;

    [SerializeField] private EmptyAction _channelEffect;

    private void OnEnable()
    {
        _channelEffect?.Sucription(HandleShoot);
    }

    private void OnDisable()
    {
        _channelEffect?.Unsuscribe(HandleShoot);
    }

    private void HandleShoot()
    {
        _effect.Play();
    }
}
