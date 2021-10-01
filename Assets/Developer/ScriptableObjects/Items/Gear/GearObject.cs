using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.Items
{
    public abstract class GearObject : ScriptableObject
    {
        [Header("ItemObject Data")]
        public int Id = -1;
        public string ItemName;
        public GearType ItemType;
        [TextArea(5, 20)]
        public string ItemDescription;
        public Sprite sprite;
        public Color Color;
        public GameObject LootObject;

        [Header("Gear Data")]        
        public GearSlots Slot;
        public GameObject EquippedInGameObject;
        public GearBuff[] Buffs;

        private void Awake()
        {
            ItemType = GearType.Gear;
        }
        public Gear CreateGearItem()
        {
            return new Gear(this);
        }
    }
    public enum GearType
    {
        Pickup,
        Gear,
        Resource
    }
    [System.Serializable]
    public class Gear
    {
        public string Name;
        public string Description;
        public int Id = -1;
        public GearType gearType;
        public GearBuff[] buffs;

        public Gear()
        {
            Name = "";
            Description = "";
            Id = -1;
            buffs = new GearBuff[0];
        }
        public Gear(GearObject gearObject)
        {            
            gearType = gearObject.ItemType;
            Name = gearObject.ItemName;
            Description = gearObject.ItemDescription;
            Id = gearObject.Id;
            buffs = new GearBuff[gearObject.Buffs.Length];
            for(int i = 0; i < buffs.Length; i++)
            {
                buffs[i] = new GearBuff(gearObject.Buffs[i].minValue, gearObject.Buffs[i].maxValue)
                {
                    attribute = gearObject.Buffs[i].attribute
                };
            }
        }
    }

    [System.Serializable]
    public class GearBuff
    {
        public Attributes attribute;
        public int buffValue;
        public int minValue;
        public int maxValue;
        public GearBuff(int min, int max)
        {
            minValue = min;
            maxValue = max;
            GenerateValue();
        }
        public void GenerateValue()
        {
            buffValue = UnityEngine.Random.Range(minValue, maxValue);
        }
        public string Stringify()
        {
            return $"{attribute}: {buffValue}"; 
        }
    }

    public enum Attributes
    {
        Agility,
        Intellect,
        Stamina,
        Strength,
    }
}
