using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.Items
{
    public abstract class Gear : ItemObject
    {
        [Header("Gear Data")]        
        [SerializeField] public GearSlots Slot;
        [SerializeField] public GameObject EquippedInGameObject;
        private void Awake()
        {
            ItemType = ItemType.Gear;
        }
    }
}
