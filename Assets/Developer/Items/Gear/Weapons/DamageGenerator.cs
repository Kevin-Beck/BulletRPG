using System;

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
            return $"Damage\n{type}: {min:F1}-{max:F1}";
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
        public float percentRemoved;
        public float minRemoved;
        public float maxRemoved;
        public DamageMitigator(DamageType type, float percentageReduced)
        {
            damageType = type;
            percentRemoved = (float)Math.Round(percentageReduced);
        }
        public string Stringify()
        {
            return $"Resistance\n{damageType}:  {percentRemoved:F0}%";
        }
    }
}

