using BulletRPG.Characters.NPC;
using BulletRPG.Gear;
using BulletRPG.Gear.Weapons;
using BulletRPG.Gear.Weapons.RangedWeapons;
using UnityEngine;

namespace BulletRPG.Characters.NPC
{
    public class BaseNPCShoot : MonoBehaviour, INPCShoot
    {
        Animator animator;
        [SerializeField] GameObject projectile;
        [SerializeField] Vector3 Offset;
        [SerializeField] float projectileSpeed;
        [SerializeField] DamageGenerator generator;
        private NPC myNPC;
        private void Awake()
        {
            myNPC = GetComponent<NPC>();
            if (myNPC == null)
            {
                myNPC = GetComponentInParent<NPC>();
            }
            animator = GetComponentInChildren<Animator>();

        }

        public void StartAttackAnimation()
        {
            Debug.Log("Starting Animation");
            animator.SetBool("shoot", true);
        }

        public void FireAttack()
        {
            Debug.Log("ActuallyFiring");
            Vector3 trueOffset = new Vector3();
            trueOffset += transform.forward * Offset.z;
            trueOffset += transform.right * Offset.x;
            trueOffset += transform.up * Offset.y;
            var bullet = Instantiate(projectile, transform.position + trueOffset, Quaternion.identity);
            bullet.transform.rotation = Quaternion.identity;
            var settings = bullet.GetComponent<BasicBullet>();
            settings.BulletSpeed = projectileSpeed;
            settings.damage = generator.GetDamage();
            bullet.transform.rotation = GetComponent<Rigidbody>().rotation;

        }
    }
}