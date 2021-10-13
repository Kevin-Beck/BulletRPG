using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    [CreateAssetMenu(fileName = "Wand", menuName = "Gear/Ranged Weapon/Wand")]
    public class WandObject : RangedWeaponObject
    {
        private void Awake()
        {
            geartype = GearType.Wand;
        }
    }
}