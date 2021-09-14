using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public abstract class Gear : ScriptableObject
    {
        public string ItemName;
        public string ItemDescription;
        [SerializeField] public GearSlots Slot;
        [SerializeField] public GameObject LootObject;
        [SerializeField] public GameObject InGameObject;
    }
}
