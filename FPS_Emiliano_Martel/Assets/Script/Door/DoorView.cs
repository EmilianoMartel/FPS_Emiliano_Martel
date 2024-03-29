using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _openParameterName = "isOpen";
    [SerializeField] private DoorLogic _logic;

    private void OnEnable()
    {
        _logic.isOpen += HandleOpen;
    }

    private void OnDisable()
    {
        _logic.isOpen -= HandleOpen;
    }

    private void Awake()
    {
        if (!_animator)
        {
            Debug.LogError($"{name}: Animator is null\nCheck and assigned one\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_logic)
        {
            Debug.LogError($"{name}: DoorLogic is null\nCheck and assigned one\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void HandleOpen(bool isOpen)
    {
        _animator.SetBool(_openParameterName, isOpen);
    }
}
