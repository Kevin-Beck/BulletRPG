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
        healthChangedEvent.Raise();
    }
    private void OnCollisionEnter(Collision collision)
    {
        CurrentHealth.Value = CurrentHealth.Value - collision.impulse.sqrMagnitude / 10;
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
            if(deathEvent != null)
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
    }
}