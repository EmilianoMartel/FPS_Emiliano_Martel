using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RayLayerObserver : MonoBehaviour
{
    [SerializeField] private LayerMask _observerLayer;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private Transform _look;

    public Action<bool> isViewLayer = delegate { };

    private void Update()
    {
        if (Physics.Raycast(_look.transform.position,_look.transform.forward,_distance,_observerLayer))
        {
            isViewLayer?.Invoke(true);
            return;
        }
        else
        {
            isViewLayer?.Invoke(false);
        }
    }
}