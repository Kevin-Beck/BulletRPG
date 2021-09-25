using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items.RangedWeapons
{
    [Serializable]
    public abstract class RangedWeaponObject : GearObject
    {
        [Header("RangedWeapon Data")]
        public GameObject Projectile;
        public float Cooldown;
        public float Speed;
        public float DamageMultiplier;
    }
}

