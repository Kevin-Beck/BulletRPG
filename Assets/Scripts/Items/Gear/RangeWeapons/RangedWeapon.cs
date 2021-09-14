using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "NewGear", menuName = "Item/Ranged Weapon")]
    public class RangedWeapon : Gear
    {
        [SerializeField] public GameObject Projectile;
        [SerializeField] public float Cooldown;
        [SerializeField] public float Speed;
        [SerializeField] public float DamageMultiplier;
    }
}

