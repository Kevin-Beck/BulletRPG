using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    [CreateAssetMenu(fileName = "newWand", menuName = "Item/Gear/Wand")]
    public class WandObject : RangedWeaponObject
    {
        private void Awake()
        {
            gearSlot = GearSlot.Wand;
            itemType = ItemType.Gear;
        }
    }
}