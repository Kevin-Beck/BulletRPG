using BulletRPG.Gear.Armor;
using BulletRPG.Gear.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters
{    
    public interface IHealth
    {

        public void SetCurrentAndMaxHealth(float currentHealth, float maxHealth);
        public Tuple<float, float> GetCurrentAndMaxHealth();
        public void HealFlatAmount(float amount);
        public void HealPercentage(float percentage);
        public void AddHealthBar(IHealthBar healthBar);
        public void ProcessDamage(Damage damage);
        public void AddDamageMitigators(List<DamageMitigator> mitigators);
    }
}

