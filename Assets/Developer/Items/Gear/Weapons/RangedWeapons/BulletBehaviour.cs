using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    [RequireComponent(typeof(Collider))]
    public class BulletBehaviour : MonoBehaviour
    {
        [HideInInspector] public float BulletSpeed;
        [HideInInspector] public Damage damage;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided");
            var health = other.GetComponent<IHealth>();
            if(health != null)
            {
                health.ProcessDamage(damage);
                Destroy(gameObject);
            }
        }
        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
        }
    }
}

