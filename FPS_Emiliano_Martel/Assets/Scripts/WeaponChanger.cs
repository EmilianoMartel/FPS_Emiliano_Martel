using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour, IInteract
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private VisualEffectsGunController _controller;
    [SerializeField] private GunSlot gunSlot;

    private void OnEnable()
    {
        DesactiveWeapon();
    }

    public void InteractiveMoment()
    {
        gunSlot.ChangeGun(this);
    }

    public void ActiveWeapon()
    {
        _weapon.enabled = true;
        _controller.enabled = true;
    }

    public void DesactiveWeapon()
    {
        _weapon.enabled = false;
        _controller.enabled = false;
    }
}
