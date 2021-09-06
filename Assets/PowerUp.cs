using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType myType;
    void OnTriggerEnter(Collider other)
    {
        if (myType == PowerUpType.AddHealth)
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(50f);
                Destroy(gameObject);
            }
        }
        else if (myType == PowerUpType.AddSpeed)
        {
            var playerMovement = other.gameObject.GetComponent<PlayerMove>();
            if (playerMovement != null)
            {
               // playerMovement.SetSpeedMultiplier(1.5f, 5.0f);
                Destroy(gameObject);
            }
        }
    }
}
public enum PowerUpType
{
    AddHealth,
    AddSpeed,
}
