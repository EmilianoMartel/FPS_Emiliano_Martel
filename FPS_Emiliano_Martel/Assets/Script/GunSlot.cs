using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GunSlot : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    
    public void ChangeGun(Weapon weapon)
    {
        Transform tempTransform = weapon.transform;
        weapon.transform.position = _weapon.transform.position;
        _weapon.transform.position = tempTransform.position;
        _weapon.transform.parent = null;
        weapon.transform.parent = transform;
        weapon.transform.rotation = transform.rotation;
        _weapon = weapon;
    }
}