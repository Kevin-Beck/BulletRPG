using System;
using UnityEngine;

namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    [Serializable]
    public abstract class RangedWeaponObject : GearObject
    {
        [Header("RangedWeapon Data")]
        public GameObject Projectile;
        public float Cooldown;
        public float ProjectileSpeed;
        public float MinDamageBottom;
        public float MinDamageTop;
        public float DamageRange;
        public DamageTypes[] damageTypes;
    }
    [System.Serializable]
    public class RangedWeapon : Gear
    {
        // TODO do we need these three things pass into the instance (i think so, but idk)
        public GameObject projectile;
        public float coolDown;
        public float projectileSpeed;

        public DamageGenerator damage;
        public RangedWeapon() : base()
        { }
        public RangedWeapon(RangedWeaponObject rangedWeaponObject) : base(rangedWeaponObject)
        {
            projectile = rangedWeaponObject.Projectile;
            coolDown = rangedWeaponObject.Cooldown;
            projectileSpeed = rangedWeaponObject.ProjectileSpeed;

            var minDamage = UnityEngine.Random.Range(rangedWeaponObject.MinDamageBottom, rangedWeaponObject.MinDamageTop);
            var maxDamage = minDamage + rangedWeaponObject.DamageRange;
            var type = rangedWeaponObject.damageTypes[UnityEngine.Random.Range(0, rangedWeaponObject.damageTypes.Length)];
            damage = new DamageGenerator(minDamage, maxDamage, type);
        }
    }
}

