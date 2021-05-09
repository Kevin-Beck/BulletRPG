using BulletRPG.NPCBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    [SerializeField] bool destroyOnDeath = true;
    [SerializeField] GameEvent destroyedEvent;
    private HealthBar healthBar;
    public float currentHealth;

    private void Start()
    {
        if(startingHealth == 0)
        {
            Debug.LogWarning($"Starting Health of {gameObject} is zero.");
        }
        currentHealth = startingHealth;
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth / startingHealth);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        currentHealth -= collision.impulse.sqrMagnitude / 10;
        if(healthBar != null)
        {
            healthBar.UpdateBar(currentHealth / startingHealth);
        }
        if(destroyOnDeath && currentHealth <= 0)
        {
            if(destroyedEvent != null)
            {
                destroyedEvent.Raise();
            }
            Destroy(gameObject);
        }
    }
    public void AddHealthBar(HealthBar bar)
    {
        healthBar = bar;
    }
}
