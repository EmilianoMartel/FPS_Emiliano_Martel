using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class CharacterActivate : MonoBehaviour
{
    [SerializeField] private FirstPersonController _character;
    [SerializeField] private Transform _look;
    [SerializeField] private float _distance;
    [SerializeField] private EmptyAction _interactEvent;

    public Action viewInteractObject = delegate { };

    private void OnEnable()
    {
        _interactEvent.Sucription(ViewActiveObject);
    }

    private void OnDisable()
    {
        _interactEvent.Unsuscribe(ViewActiveObject);
    }

    private void Awake()
    {
        if (!_character)
        {
            Debug.LogError($"{name}: First person controller is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void ViewActiveObject()
    {
        if (Physics.Raycast(_look.position, _look.forward, out var hit, _distance))
        {
            if (hit.transform.TryGetComponent<IInteract>(out IInteract interact))
            {
                interact.InteractiveMoment();
            }
        }
    }
}