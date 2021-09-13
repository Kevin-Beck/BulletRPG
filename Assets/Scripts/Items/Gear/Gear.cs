using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    [CreateAssetMenu(fileName = "NewGear", menuName = "Item/Gear")]
    public class Gear : ScriptableObject
    {
        public string Name;
        public string Description;
        [SerializeField] public GearSlots Slot;
        [SerializeField] public GameObject LootObject;
        [SerializeField] public GameObject VisualObject;
    }
}
