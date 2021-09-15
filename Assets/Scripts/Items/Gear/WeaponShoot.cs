using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletRPG.Items
{
    public class WeaponShoot : MonoBehaviour, IAttack
    {
        [SerializeField] RangedWeapon rangedWeapon;
        
        private bool canShoot = true;

        PlayerInputActions playerInputActions;
        Animator animator;

        private void Awake()
        {
            canShoot = false;
            Invoke("CooldownReset", rangedWeapon.Cooldown);
            animator = GetComponentInParent<Animator>();            
            playerInputActions = new PlayerInputActions();
        }
        private void Start()
        {
            playerInputActions.Player.Enable();
            playerInputActions.Player.PrimaryAttack.performed += StartAttack;
        }
        public void StartAttack(InputAction.CallbackContext context)
        {
            if (context.performed && canShoot)
            {
                canShoot = false;
                Invoke("CooldownReset", rangedWeapon.Cooldown);
                Debug.Log("Attack");
                animator.SetTrigger("Default Attack");
            }
        }
        public void FireAttack()
        {
            Debug.Log("Firing Actual Attack");
            var spawn = Instantiate(rangedWeapon.Projectile, transform.position, Quaternion.identity);
            spawn.transform.rotation = Quaternion.identity;
            spawn.GetComponent<BulletMovement>().BulletSpeed = rangedWeapon.Speed;
            spawn.transform.rotation = GetComponentInParent<Rigidbody>().transform.rotation;

        }
        public void OnDestroy()
        {
            StopAllCoroutines();
            playerInputActions.Player.PrimaryAttack.performed -= StartAttack;
        }

        public void CooldownReset()
        {
            canShoot = true;
        }
    }

}