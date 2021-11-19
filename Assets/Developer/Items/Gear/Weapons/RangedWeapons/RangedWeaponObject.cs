using BulletRPG.Effects;
using System;
using UnityEngine;

namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    [Serializable]
    public abstract class RangedWeaponObject : GearObject
    {
        [Header("RangedWeapon Data")]
        public float Cooldown;
        public float ProjectileSpeed;
        public float MinDamage;
        public float MaxDamage;
        public DamageType[] damageTypes;
    }
    [System.Serializable]
    public class RangedWeapon : Gear
    {
        public GameObject projectile;
        public float reloadTime;
        public float projectileSpeed;

        public DamageGenerator damage;
        public RangedWeapon(RangedWeaponObject rangedWeaponObject) : base(rangedWeaponObject)
        {
            reloadTime = rangedWeaponObject.Cooldown;
            projectileSpeed = rangedWeaponObject.ProjectileSpeed;

            var type = rangedWeaponObject.damageTypes[UnityEngine.Random.Range(0, rangedWeaponObject.damageTypes.Length)];
            damage = new DamageGenerator(rangedWeaponObject.MinDamage, rangedWeaponObject.MaxDamage, type);
            name = StringifyName();
            description = StringifyDescription();
        }
        public override string StringifyName()
        {            
            return base.StringifyName() + $" of {damage.type.DamageName}";
        }
        public override string StringifyDescription()
        {
            string desc = "";
            desc += $"\n\nReload Time: {reloadTime}\nBullet Speed: {projectileSpeed}\n";
            desc += $"\n{damage.Stringify()}";
            return base.StringifyDescription() + desc;
        }
    }
}

