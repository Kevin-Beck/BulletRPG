using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear.Weapons
{
    [System.Serializable]
    public class DamageGenerator
    {
        public float min;
        public float max;
        public DamageTypes type;
        public DamageGenerator(float minimumDamage, float maximumDamage, DamageTypes damageType)
        {
            type = damageType;
            min = minimumDamage;
            max = maximumDamage;
        }
        public Damage GetDamage()
        {
            return new Damage(UnityEngine.Random.Range(min, max + 1), type);
        }
    }

    public class Damage
    {
        public float amount;
        public DamageTypes damageType;
        public Damage(float amountOfDamage, DamageTypes typeOfDamage)
        {
            amount = amountOfDamage;
            damageType = typeOfDamage;
        }
    }
}

