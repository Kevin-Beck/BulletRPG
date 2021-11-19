using BulletRPG.Elements.Projectiles;
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

