using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    [RequireComponent(typeof(Collider))]
    public abstract class BulletBehavior : MonoBehaviour
    {
        public abstract void OnTriggerEnter(Collider other);
        public abstract void Update();
    }
}

