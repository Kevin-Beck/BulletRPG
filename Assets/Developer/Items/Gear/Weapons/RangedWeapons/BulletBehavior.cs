using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Gear.Weapons.RangedWeapons
{

    [RequireComponent(typeof(Collider))]
    public abstract class BulletBehavior : MonoBehaviour
    {
        [HideInInspector] public float BulletSpeed;
        [HideInInspector] public Damage damage;
        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided");
            var health = other.GetComponent<IHealth>();
            if (health != null)
            {
                health.ProcessDamage(damage);
                Destroy(gameObject);
            }
        }
        public abstract void Update();
        private void Awake()
        {
            var collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }
    }
}

