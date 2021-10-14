using BulletRPG.Characters.Player;
using UnityEngine;

namespace BulletRPG.Gear
{
    public abstract class GearObject : ItemObject
    {       
        [Header("Gear Data")]        
        public GearSlot gearSlot;
        public GameObject EquippedInGameObject;
        public GearBuff[] Buffs;

        private void Awake()
        {
            itemType = ItemType.Gear;
        }
        public Gear CreateGearItem()
        {
            return new Gear(this);
        }
    }
    public enum GearSlot
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
    public class Gear : Item
    {
        public GearSlot gearSlot;
        public bool IsStackable;
        public GearBuff[] buffs;
        private string gearDescription;

        public Gear() : base()
        {
            gearSlot = GearSlot.Default;
            buffs = new GearBuff[0];
        }
        public Gear(GearObject gearObject) : base(gearObject)
        {
            gearSlot = gearObject.gearSlot;
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
