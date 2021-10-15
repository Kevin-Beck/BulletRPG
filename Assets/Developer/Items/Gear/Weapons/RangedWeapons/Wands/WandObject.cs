using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    [CreateAssetMenu(fileName = "newWand", menuName = "Gear/Wand")]
    public class WandObject : RangedWeaponObject
    {
        [Header("WandObject Data")]
        public List<WandProjectile> projectileSet = new List<WandProjectile>();
        private void Awake()
        {
            gearSlot = GearSlot.Wand;
            itemType = ItemType.Gear;
        }
        public Wand CreateInstance(WandObject wandObject)
        {
            return new Wand(wandObject);
        }
    }
    [System.Serializable]
    public class WandProjectile
    {
        public DamageType damageType;
        public GameObject damageProjectilePrefab;
    }
    [System.Serializable]
    public class Wand : RangedWeapon
    {
        public Wand(WandObject wandObject) : base(wandObject)
        {
            for(int i = 0; i < wandObject.projectileSet.Count; i++)
            {
                if(damage.type == wandObject.projectileSet[i].damageType)
                {
                    projectile = wandObject.projectileSet[i].damageProjectilePrefab;
                }
            }
        }
    }
}