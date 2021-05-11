using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType myType;
    void OnCollisionEnter(Collision collision)
    {
        if(myType == PowerUpType.AddHealth)
        {
            var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(50f);
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
