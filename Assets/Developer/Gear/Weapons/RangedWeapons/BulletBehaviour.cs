using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    public class BulletBehaviour : MonoBehaviour
    {
        [HideInInspector] public float BulletSpeed;
        [HideInInspector] public Damage damage;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("NOT SET UP");
        }
        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
        }
    }
}

