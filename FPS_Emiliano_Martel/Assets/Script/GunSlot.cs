using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GunSlot : MonoBehaviour
{
    [SerializeField] private WeaponChanger _weaponChanger;

    private void OnEnable()
    {
        StartCoroutine(WaitForEnable());
    }

    public void ChangeGun(WeaponChanger newWeapon)
    {
        Transform tempTransform = newWeapon.transform;
        newWeapon.transform.position = _weaponChanger.transform.position;
        _weaponChanger.transform.position = tempTransform.position;
        _weaponChanger.transform.parent = null;
        newWeapon.transform.parent = transform;
        newWeapon.transform.rotation = transform.rotation;
        _weaponChanger.DesactiveWeapon();
        newWeapon.ActiveWeapon();
        _weaponChanger = newWeapon;
    }

    private IEnumerator WaitForEnable()
    {
        yield return new WaitForSeconds(1);
        _weaponChanger.ActiveWeapon();
    }
}