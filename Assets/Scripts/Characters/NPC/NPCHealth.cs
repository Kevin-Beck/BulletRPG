using UnityEngine;

namespace BulletRPG.Characters.NPC
{
    [RequireComponent(typeof(Collider))]
    public class NPCHealth : MonoBehaviour
    {
        [SerializeField] public float startingHealth;
        [SerializeField] bool destroyOnDeath = true;
        [SerializeField] GameEvent destroyedEvent;
        private NPCHealthBar healthBar;
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

        public void AddHealthBar(NPCHealthBar bar)
        {
            healthBar = bar;
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
    }
}



