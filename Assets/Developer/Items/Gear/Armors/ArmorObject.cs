using BulletRPG.Gear.Weapons;
using System;
using System.Collections;
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
            float mitigationAmount = UnityEngine.Random.Range(armorObject.PossibleDamageMitigations[mitigatorIndex].minRemoved, armorObject.PossibleDamageMitigations[mitigatorIndex].maxRemoved);
            damageMitigators.Add(new DamageMitigator(armorObject.PossibleDamageMitigations[mitigatorIndex].damageType, mitigationAmount));

            var getNameAndDesc = Utilities.NameAndDescribe(this);
            name = getNameAndDesc.Item1;
            description = getNameAndDesc.Item2;
        }
    }
}

