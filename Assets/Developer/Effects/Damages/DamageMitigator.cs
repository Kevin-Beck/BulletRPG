using System;
using UnityEngine;

namespace BulletRPG.Effects
{
    [System.Serializable]
    public class DamageMitigator
    {
        public DamageType damageType;
        public ImmunityLevels protectionLevel;
        public float PercentReduced { get => (float)Math.Round((float)(int)protectionLevel / 100f, 2); }
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
