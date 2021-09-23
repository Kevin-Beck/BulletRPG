using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Items.RangedWeapons
{
    [CreateAssetMenu(fileName = "Wand", menuName = "Item/Ranged Weapon/Wand")]
    public class WandObject : RangedWeaponObject
    {
        private void Awake()
        {
            Slot = GearSlots.Wand;
        }
    }
}

