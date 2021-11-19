using BulletRPG.Effects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear.Armor
{
    [Serializable]
    public abstract class ArmorObject : GearObject
    {
        [Header("Armor Data")]
        public List<DamageMitigator> PossibleDamageMitigations;
    }
    public class Armor : Gear
    {
        public List<DamageMitigator> damageMitigators;

        public Armor(ArmorObject armorObject) : base(armorObject)
        {
            damageMitigators = new List<DamageMitigator>();
            int mitigatorIndex = UnityEngine.Random.Range(0, armorObject.PossibleDamageMitigations.Count);
            ImmunityLevels immunityLevel = armorObject.PossibleDamageMitigations[mitigatorIndex].protectionLevel;
            damageMitigators.Add(new DamageMitigator(armorObject.PossibleDamageMitigations[mitigatorIndex].damageType, immunityLevel));

            name = StringifyName();
            description = StringifyDescription();
        }
        public override string StringifyName()
        {
            // TODO name different combos of damage reduction
            string nameFromArmor = "";
            if (damageMitigators.Count > 0)
            {
                // TODO rebuild damage resistance into a class
                //if(damageMitigators[0].damageType != DamageType.Regular)
                //{
                //    nameFromArmor = $" of {damageMitigators[0].damageType}";
                //}
                //else
                //{
                //    nameFromArmor = $" of Protection";
                //}
            }
            return base.StringifyName() + nameFromArmor;
        }
        public override string StringifyDescription()
        {
            string desc = "";
            foreach(DamageMitigator dm in damageMitigators)
            {
                desc += $"\n\n{dm.Stringify()}";
            }
            return base.StringifyDescription() + desc;
        }
    }
}

