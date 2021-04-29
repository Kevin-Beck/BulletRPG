﻿using BulletRPG.NPCBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{
    [SerializeField] public FloatVariable StartingHealth;
    [SerializeField] bool destroyOnDeath = true;
    [SerializeField] GameEvent destroyedEvent;
    private HealthBar healthBar;
    public float currentHealth;

    private void Start()
    {
        currentHealth = StartingHealth.Value;
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth / StartingHealth.Value);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        currentHealth = currentHealth - collision.impulse.sqrMagnitude / 10;
        if(healthBar != null)
        {
            healthBar.UpdateBar(currentHealth / StartingHealth.Value);
        }
        if(destroyOnDeath && currentHealth <= 0)
        {
            if(destroyedEvent != null)
            {
                destroyedEvent.Raise();
            }
            Destroy(this.gameObject);
        }
    }
    public void AddHealthBar(HealthBar bar)
    {
        healthBar = bar;
    }
}