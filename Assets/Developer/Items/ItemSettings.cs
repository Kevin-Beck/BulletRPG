using BulletRPG.Gear;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName ="Item Settings", menuName = "Settings/ItemSettings")]
    public class ItemSettings : ScriptableObject
    {
        [Header("Color Settings")]
        public RarityColors rarityColors;
        public DamageTypeColors damageTypeColors;
        [Header("LootableObjects")]
        public LootableItemTable lootableItems;
    }
}

