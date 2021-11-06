using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    public class BasicBullet : BulletBehavior
    {
        [HideInInspector] public float BulletSpeed;
        [HideInInspector] public Damage damage;

        public override void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided");
            var health = other.GetComponent<IHealth>();
            if(health != null)
            {
                health.ProcessDamage(damage);
                Destroy(gameObject);
            }
        }
        public override void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
        }
    }
}

