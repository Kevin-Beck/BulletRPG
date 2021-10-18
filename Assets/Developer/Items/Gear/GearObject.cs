using BulletRPG.Characters.Player;
using UnityEngine;

namespace BulletRPG.Gear
{
    public abstract class GearObject : ItemObject
    {
        [Header("Gear Data")]
        public Rarity rarity;
        public GearSlot gearSlot;
        public GameObject EquippedInGameObject;

        private void Awake()
        {
            config.itemType = ItemType.Gear;
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
        public GearBuff[] buffs;
        public Rarity rarity;

        public Gear() : base()
        {
            gearSlot = GearSlot.Default;
            buffs = new GearBuff[0];
            rarity = Rarity.Artefact;            
        }
        public Gear(GearObject gearObject) : base(gearObject)
        {
            buffs = GetBuffs(gearObject.rarity);
            gearSlot = gearObject.gearSlot;
            rarity = gearObject.rarity;
        }
        public override string StringifyName()
        {
            return base.StringifyName();
        }
        public override string StringifyDescription()
        {
            string desc = "";
            foreach (GearBuff buff in buffs)
            {
                desc += buff.Stringify() + "\n";
            }
            return (base.StringifyDescription() + $"\n\n{desc}").Trim();
        }
        private GearBuff[] GetBuffs(Rarity rarity)
        {
            buffs = new GearBuff[GetBuffCount(rarity)];
            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i] = new GearBuff(rarity);
            }
            return buffs;
        }
        public int GetBuffCount(Rarity rarity) => rarity switch
        {
            Rarity.Common => 1,
            Rarity.Uncommon => 2,
            Rarity.Rare => 2,
            Rarity.Epic => 3,
            Rarity.Legendary => 3,
            Rarity.Artefact => 4,
            _ => -1,
        };
    }
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artefact,
    }

    [System.Serializable]
    public class GearBuff : IModifier
    {
        public AttributeType attribute;
        public int buffValue;
        public GearBuff(Rarity rarity)
        {
            attribute = (AttributeType)Random.Range(0, (int)AttributeType.COUNT);
            var minmax = GetMinMaxValues(rarity);
            GenerateValue(minmax);
        }

        public void AddValue(ref int value)
        {
            value += buffValue;
        }

        public void GenerateValue((int,int) values)
        {            
            buffValue = UnityEngine.Random.Range(values.Item1, values.Item2+1);
        }
        public string Stringify()
        {
            return $"{attribute}: {buffValue}"; 
        }
        public static (int, int) GetMinMaxValues(Rarity rarity) => rarity switch
        {
            Rarity.Common => (1, 3),
            Rarity.Uncommon => (1, 3),
            Rarity.Rare => (3, 5),
            Rarity.Epic => (3, 5),
            Rarity.Legendary => (5, 10),
            Rarity.Artefact => (10, 10),
            _ => (-1, -1),
        };
    }
}
