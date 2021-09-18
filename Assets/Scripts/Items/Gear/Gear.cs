using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.Items
{
    public abstract class Gear : ScriptableObject
    {
        [Header("Core Gear Elements")]
        public string ItemName;
        public string ItemDescription;
        [SerializeField] public GearSlots Slot;
        [SerializeField] public GameObject LootObject;
        [SerializeField] public GameObject InGameObject;
        [SerializeField] public Sprite Sprite;
        [SerializeField] public Color Color;
    }
}
