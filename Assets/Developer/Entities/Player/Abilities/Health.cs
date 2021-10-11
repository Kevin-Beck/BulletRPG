using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Characters.Player
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] FloatVariable MaxHealth;
        [SerializeField] FloatVariable CurrentHealth;
        [SerializeField] bool destroyOnDeath = true;
        [SerializeField] GameEvent deathEvent;
        [SerializeField] GameEvent healthChangedEvent;
        [SerializeField] IHealthBar healthBar;

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
                if (destroyOnDeath)
                {
                    Destroy(gameObject);
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
        public void HealFlatAmount(float amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot heal for less than 0");
                return;
            }
            ChangeHealth(amount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentage">0 to 100</param>
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
            ChangeHealth(MaxHealth.Value * (percentage/100));
        }
        public void TakeDamageAmount(float amount)
        {
            if (amount < 0)
            {
                Debug.Log("Cannot damage for less than 0");
                return;
            }
            ChangeHealth(-1 * amount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentage">0 to 100</param>
        public void TakeDamagePercentage(float percentage)
        {
            if (percentage < 0f)
            {
                Debug.Log("Cannot damage with percent less than 0");
                return;
            }
            if (percentage > 100f)
            {
                percentage = 100f;
            }
            ChangeHealth(MaxHealth.Value * (percentage/100f) * -1);
        }
        public void AddHealthBar(IHealthBar bar)
        {
            healthBar = bar;
        }
    }
}