using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Parameters")]
    [Tooltip("Weapon damage")]
    [SerializeField] protected int p_damage;
    [Tooltip("What layers are enemies.")]
    [SerializeField] protected LayerMask p_enemyMask;
    [SerializeField] protected GunSlot p_gunSlot;

    [Header("Channels")]
    [SerializeField] protected EmptyAction p_clickMoment;

    [Header("Manager")]
    [SerializeField] protected FirstPersonController p_firstPersonController;
}