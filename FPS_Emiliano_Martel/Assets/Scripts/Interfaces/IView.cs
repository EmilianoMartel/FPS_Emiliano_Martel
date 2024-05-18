using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView
{
    private bool ViewObject(Transform startPoint,float distance, LayerMask viewMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPoint.position, startPoint.forward, out hit, distance, viewMask))
        {
            return true;
        }
        return false;
    }
}
