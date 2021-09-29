using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BulletRPG.Items
{
    public abstract class GearObject : ItemObject
    {
        [Header("Gear Data")]        
        public GearSlots Slot;
        public GameObject EquippedInGameObject;
        public GearBuff[] Buffs;

        private void Awake()
        {
            ItemType = ItemType.Gear;
        }
        public Gear CreateGearItem()
        {
            return new Gear(this);
        }
    }

    [System.Serializable]
    public class Gear : Item
    {
        public GearBuff[] buffs;
        public Gear(GearObject gearObject) : base(gearObject)
        {
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
