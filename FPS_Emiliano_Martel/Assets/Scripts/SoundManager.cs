using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _menuSound;
    [SerializeField] private AudioSource _gameSound;
    [SerializeField] private AudioSource _endSound;


    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void HandleMenuSound()
    {
        _menuSound.Play();
    }
}
