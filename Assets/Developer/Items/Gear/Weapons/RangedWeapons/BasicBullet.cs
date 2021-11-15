using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    public class BasicBullet : BulletBehavior
    {
        public override void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * BulletSpeed);
        }
    }
}

