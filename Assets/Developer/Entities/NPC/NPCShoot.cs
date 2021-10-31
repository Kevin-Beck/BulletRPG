using BulletRPG.Characters.NPC;
using BulletRPG.Gear;
using BulletRPG.Gear.Weapons;
using BulletRPG.Gear.Weapons.RangedWeapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCShoot : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] DamageGenerator generator;
    private NPC myNPC;

    private void Awake()
    {
        myNPC = GetComponent<NPC>();
        animator = GetComponentInChildren<Animator>();
    }

    public void StartAttackAnimation()
    {
        Debug.Log("Starting Animation");
        animator.SetTrigger("shoot");
    }

    public void FireAttack()
    {
        Debug.Log("ActuallyFiring");
        var bullet = Instantiate(projectile, transform.position + transform.forward + Vector3.up, Quaternion.identity);
        bullet.transform.rotation = Quaternion.identity;
        var settings = bullet.GetComponent<BulletBehaviour>();
        settings.BulletSpeed = projectileSpeed;
        settings.damage = generator.GetDamage();
        bullet.transform.rotation = GetComponentInParent<Rigidbody>().transform.rotation;
        
    }
}
