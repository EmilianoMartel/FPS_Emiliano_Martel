using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void ActiveParticles()
    {
        _particleSystem.Play();
    }
}
