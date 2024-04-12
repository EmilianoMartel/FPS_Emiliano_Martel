using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Parameters")]
    [Tooltip("Weapon damage")]
    [SerializeField] protected int p_damage;
    [Tooltip("What layers are enemies.")]
    [SerializeField] protected LayerMask p_enemyMask;
    [SerializeField] protected GunSlot p_gunSlot;

    [Header("Channels")]
    [SerializeField] protected EmptyAction p_clickMoment;
    [SerializeField] protected BoolChanelSo p_isTriggerEvent;

    [Header("Manager")]
    [SerializeField] protected FirstPersonController p_firstPersonController;

    protected bool  p_isPressTrigger;

    protected virtual void OnEnable()
    {
        if (p_isTriggerEvent)
            p_isTriggerEvent.Sucription(HandleSetPressTrigger);
    }

    protected virtual void OnDisable()
    {
        if(p_isTriggerEvent)
            p_isTriggerEvent.Unsuscribe(HandleSetPressTrigger);
    }

    /// <summary>
    /// This function handle the moment when the "shoot button" was pressed and do a logic.
    /// The class have a protected bool by the name "p_isPressTrigger"
    /// </summary>
    /// <param name="pressTrigger"></param>
    protected virtual void HandleSetPressTrigger(bool pressTrigger)
    {
        p_isPressTrigger = pressTrigger;
    }

    public virtual void SendWeaponParameters()
    {

    }
}