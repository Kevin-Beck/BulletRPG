using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [Serializable]
    [CreateAssetMenu(fileName = "NewGear", menuName = "Item/Ranged Weapon")]
    public class RangedWeapon : Gear
    {
        [Header("Ranged Weapon Elements")]
        [SerializeField] public GameObject Projectile;
        [SerializeField] public float Cooldown;
        [SerializeField] public float Speed;
        [SerializeField] public float DamageMultiplier;
    }
}

