using BulletRPG.Characters.Player;
using UnityEngine;

namespace BulletRPG.Gear
{
    public abstract class GearObject : ScriptableObject
    {
        [Header("ItemObject Data")]
        public int Id = -1;
        public string ItemName;
        public GearType ItemType;
        public bool IsStackable;
        [TextArea(5, 20)]
        public string ItemDescription;
        public Sprite sprite;
        public Color Color;
        public GameObject LootObject;

        [Header("Gear Data")]        
        public GearType geartype;
        public GameObject EquippedInGameObject;
        public GearBuff[] Buffs;

        private void Awake()
        {
            ItemType = GearType.Default;
        }
        public Gear CreateGearItem()
        {
            return new Gear(this);
        }
    }
    public enum GearType
    {
        Default,
        Wand, // Ranged weapon
        Bow, // Ranged weapon
        Javelin, // Ranged weapon
        Focus, // Enhances ranged weapon position
        Helmet, // Head/hat that provides protection
        Shield, // Provides protection
        Alteration, // Magical shields/auras that provide areas of increased ability, speed increases
        Conjuration, // summons physical barriers, allies to fight for you, Conjure ranged weapon if WandSlot available
        Destruction, // damage spells
        Illusion, // convert enemies to fight for you, invisibility, copy yourself
        Mysticism, // teleport, absorb damage and reflect it, time slow
        Restoration // passive healing/protection items
    }
    [System.Serializable]
    public class Gear
    {
        public string Name;
        public string Description;
        public int Id = -1;
        public GearType gearType;
        public bool IsStackable;
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
            IsStackable = gearObject.IsStackable;
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
    public class GearBuff : IModifier
    {
        public AttributeType attribute;
        public int buffValue;
        public int minValue;
        public int maxValue;
        public GearBuff(int min, int max)
        {
            minValue = min;
            maxValue = max;
            GenerateValue();
        }

        public void AddValue(ref int value)
        {
            value += buffValue;
        }

        public void GenerateValue()
        {
            buffValue = UnityEngine.Random.Range(minValue, maxValue + 1);
        }
        public string Stringify()
        {
            return $"{attribute}: {buffValue}"; 
        }
    }
}
