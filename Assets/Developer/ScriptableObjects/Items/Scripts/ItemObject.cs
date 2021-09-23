using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public abstract class ItemObject : ScriptableObject
    {
        [Header("ItemObject Data")]
        public string ItemName;
        public ItemType ItemType;
        [TextArea(5, 20)]
        public string ItemDescription;

        [SerializeField] public Sprite Sprite;
        [SerializeField] public Color Color;
        [SerializeField] public GameObject LootObject;
    }

    public enum ItemType
    {
        Pickup,
        Gear,
        Resource
    }
}
