using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items.RangedWeapons
{
    [Serializable]
    public abstract class RangedWeaponObject : Gear
    {
        [Header("RangedWeapon Data")]
        [SerializeField] public GameObject Projectile;
        [SerializeField] public float Cooldown;
        [SerializeField] public float Speed;
        [SerializeField] public float DamageMultiplier;
    }
}

