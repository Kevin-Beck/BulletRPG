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
    [SerializeField] Vector3 SpawnPointOffset;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] DamageGenerator generator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void StartAttackAnimation()
    {
        // TODO clean up this mess, Kevin
        Debug.Log("Starting Animation");
        if(animator == null)
        {
            Debug.Log("null animator1");
            animator = GetComponentInParent<Animator>();
        }

        if(animator != null)
        {            
            animator.SetTrigger("baseattack");
        }
        else
        {
            Debug.Log("null animator2");
        }
    }

    public void FireAttack()
    {
        Debug.Log("ActuallyFiring");
        var bullet = Instantiate(projectile, transform.position + SpawnPointOffset, Quaternion.identity);
        bullet.transform.rotation = Quaternion.identity;
        var settings = bullet.GetComponent<BulletBehaviour>();
        settings.BulletSpeed = projectileSpeed;
        settings.damage = generator.GetDamage();

        bullet.transform.rotation = GetComponentInParent<Rigidbody>().transform.rotation;
    }
    public void Continue()
    {
        //me.Continue();
    }
}
