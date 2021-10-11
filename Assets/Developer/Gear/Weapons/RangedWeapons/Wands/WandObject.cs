using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Gear.RangedWeapons
{
    [CreateAssetMenu(fileName = "Wand", menuName = "Item/Ranged Weapon/Wand")]
    public class WandObject : RangedWeaponObject
    {
        private void Awake()
        {
            geartype = GearType.Wand;
        }
    }
}

