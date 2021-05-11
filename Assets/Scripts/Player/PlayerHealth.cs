using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] FloatVariable MaxHealth;
    [SerializeField] FloatVariable CurrentHealth;
    [SerializeField] bool destroyOnDeath = true;
    [SerializeField] GameEvent deathEvent;
    [SerializeField] GameEvent healthChangedEvent;

    private void Start()
    {
        CurrentHealth.Value = MaxHealth.Value;
        if (healthChangedEvent != null)
        {
            healthChangedEvent.Raise();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ChangeHealth(-1 * collision.impulse.sqrMagnitude / 10);
    }
    public void ChangeHealth(float healthPoints)
    {
        Debug.Log("HealthChanged by " + healthPoints);
        CurrentHealth.Value = CurrentHealth.Value + healthPoints;
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
                Destroy(this.gameObject);
            }
        }
        if(CurrentHealth.Value > MaxHealth.Value)
        {
            CurrentHealth.Value = MaxHealth.Value;
        }
    }
}