using BulletRPG.Gear.Armor;
using BulletRPG.Gear.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters.Player
{
    public class Health : MonoBehaviour, IHealth
    {
        public FloatVariable MaxHealth;
        public FloatVariable CurrentHealth;
        public GameEvent deathEvent;
        public GameEvent healthChangedEvent;
        public IHealthBar healthBar;
        public List<DamageMitigator> damageMitigators = new List<DamageMitigator>();

        private void Start()
        {
            CurrentHealth.Value = MaxHealth.Value;
            if (healthChangedEvent != null)
            {
                healthChangedEvent.Raise();

            }
        }
        public void SetCurrentAndMaxHealth(float currentHealth, float maxHealth)
        {
            MaxHealth.SetValue(maxHealth);
            CurrentHealth.SetValue(currentHealth);
        }
        public Tuple<float, float> GetCurrentAndMaxHealth()
        {
            return new Tuple<float, float>(CurrentHealth.Value, MaxHealth.Value);
        }
        public void ChangeHealth(float healthPoints)
        {
            CurrentHealth.Value += healthPoints;
            if (healthChangedEvent != null)
            {
                healthChangedEvent.Raise();
            }
            else
            {
                Debug.Log("Player damagedEvent is null");
            }
            if (CurrentHealth.Value <= 0)
            {
                if (deathEvent != null)
                {
                    deathEvent.Raise();
                }
                else
                {
                    Debug.Log("Player deathEvent is null");
                }
            }
            if (CurrentHealth.Value > MaxHealth.Value)
            {
                CurrentHealth.Value = MaxHealth.Value;
            }
            if(healthBar != null)
            {
                healthBar.UpdateHealthBar(CurrentHealth.Value / MaxHealth.Value);
            }
        }

        public void AddHealthBar(IHealthBar bar)
        {
            healthBar = bar;
        }

        public void ProcessDamage(Damage damage)
        {
            float reductionPercentage = 0;
            for (int i = 0; i < damageMitigators.Count; i++)
            {
                if(damageMitigators[i].damageType == damage.damageType)
                {
                    reductionPercentage += damageMitigators[i].percentRemoved;
                }
            }
            // Enough damage reduction will roll it over to healing
            float healthChange = damage.amount * (1f - reductionPercentage) * -1;
            ChangeHealth(healthChange);
        }

        public void AddDamageMitigators(List<DamageMitigator> mitigators)
        {
            damageMitigators.AddRange(mitigators);
        }
        public void HealFlatAmount(float amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot heal for less than 0");
                return;
            }
            ChangeHealth(amount);
        }
        public void HealPercentage(float percentage)
        {
            if (percentage < 0)
            {
                Debug.Log("Cannot heal with percent less than 0");
                return;
            }
            if (percentage > 100)
            {
                percentage = 100;
            }
            ChangeHealth(MaxHealth.Value * (percentage / 100));
        }
    }
}