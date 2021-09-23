using System;
using UnityEngine;

namespace BulletRPG.Characters.NPC
{
    [RequireComponent(typeof(Collider))]
    public class NPCHealth : MonoBehaviour, IHealth
    {
        [SerializeField] public float startingHealth;
        [SerializeField] bool destroyOnDeath = true;
        [SerializeField] GameEvent destroyedEvent;
        private IHealthBar healthBar;
        public float currentHealth;

        private void Start()
        {
            if (startingHealth == 0)
            {
                Debug.LogWarning($"Starting Health of {gameObject} is zero.");
            }
            currentHealth = startingHealth;
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(currentHealth / startingHealth);
            }
        }

        public void ChangeHealth(float amount)
        {
            currentHealth += amount;
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(currentHealth / startingHealth);
            }
            if (destroyOnDeath && currentHealth <= 0)
            {
                if (destroyedEvent != null)
                {
                    destroyedEvent.Raise();
                }
                Destroy(gameObject);
            }
        }

        public void HealFlatAmount(float amount)
        {
            if(amount < 0)
            {
                Debug.Log("Cannot heal for less than 0");
                return;
            }
            ChangeHealth(amount);
        }

        public void HealPercentage(float percentage)
        {
            if(percentage < 0)
            {
                Debug.Log("Cannot heal with percent less than 0");
                return;
            }
            if(percentage > 100)
            {
                percentage = 100;
            }
            ChangeHealth(startingHealth * (percentage/100));
        }

        public void TakeDamageAmount(float amount)
        {
            if(amount < 0)
            {
                Debug.Log("Cannot damage for less than 0");
                return;
            }
            ChangeHealth(-1 * amount);
        }

        public void TakeDamagePercentage(float percentage)
        {
            if (percentage < 0)
            {
                Debug.Log("Cannot damage with percent less than 0");
                return;
            }
            if (percentage > 1)
            {
                percentage = 1;
            }
            ChangeHealth(startingHealth * (percentage/100) * -1);
        }

        public void AddHealthBar(IHealthBar bar)
        {
            healthBar = bar;
        }

        public void SetCurrentAndMaxHealth(float currentHealth, float maxHealth)
        {
            throw new NotImplementedException();
        }

        public Tuple<float, float> GetCurrentAndMaxHealth()
        {
            throw new NotImplementedException();
        }
    }
}



