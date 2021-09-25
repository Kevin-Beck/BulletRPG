using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Items
{
    public abstract class ItemObject : ScriptableObject
    {
        [Header("ItemObject Data")]
        public int Id;
        public string ItemName;
        public ItemType ItemType;
        [TextArea(5, 20)]
        public string ItemDescription;
        public Sprite sprite;
        public Color Color;
        public GameObject LootObject;
    }

    public enum ItemType
    {
        Pickup,
        Gear,
        Resource
    }

    [System.Serializable]
    public class Item
    {
        public string Name;
        public string Description;
        public int Id;
        public ItemType itemType;
        public Item(ItemObject itemObject)
        {
            itemType = itemObject.ItemType;
            Name = itemObject.ItemName;
            Description = itemObject.ItemDescription;
            Id = itemObject.Id;
        }
    }
}
