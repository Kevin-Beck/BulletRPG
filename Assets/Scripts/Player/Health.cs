using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    [SerializeField] bool destroyOnDeath = true;
    private void OnCollisionEnter(Collision collision)
    {
        health = health - collision.impulse.sqrMagnitude / 10;
        if(destroyOnDeath && health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
