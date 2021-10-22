using System;
using UnityEngine;

namespace BulletRPG.Gear.Weapons
{
    [System.Serializable]
    public class DamageGenerator
    {
        public float min;
        public float max;
        public DamageType type;
        public DamageGenerator(float minimumDamage, float maximumDamage, DamageType damageType)
        {
            type = damageType;
            min = minimumDamage;
            max = maximumDamage;
        }
        public Damage GetDamage()
        {
            return new Damage(UnityEngine.Random.Range(min, max), type);
        }
        public string Stringify()
        {
            return $"Damage\n{type.DamageName}: {min:F1}-{max:F1}";
        }
    }

    public class Damage
    {
        public float amount;
        public DamageType damageType;
        public Damage(float amountOfDamage, DamageType typeOfDamage)
        {
            amount = amountOfDamage;
            damageType = typeOfDamage;
        }
    }

    [System.Serializable]
    public class DamageMitigator
    {
        public DamageType damageType;
        public ImmunityLevels protectionLevel;
        public float PercentReduced { get=> (float)Math.Round((float)(int)protectionLevel / 100f, 2); }
        public DamageMitigator(DamageType type, ImmunityLevels protectionLevel)
        {
            damageType = type;
            
        }
        public Damage MitigateDamage(Damage incoming)
        {
            if (incoming.damageType == damageType)
            {
                incoming.amount -= incoming.amount * PercentReduced;
            }
            Debug.Log("Percent Removed: " + PercentReduced);
            return incoming;
        }
        public string Stringify()
        {
            return $"Resistance\n{damageType.DamageName}:  {PercentReduced:F0}%";
        }
    }
    [Serializable]
    public enum ImmunityLevels
    {
        resistance = 50,
        immunity = 100,
    }
}

