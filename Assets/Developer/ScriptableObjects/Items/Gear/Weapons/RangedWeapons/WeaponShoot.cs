using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletRPG.Items.RangedWeapons
{
    public class WeaponShoot : MonoBehaviour, IAttack
    {
        [SerializeField] RangedWeaponObject rangedWeapon;
        private Gear gearPlaceholder;
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
            playerInputActions.Player.PrimaryAttack.performed += StartAttackFromInput;
        }
        public void StartAttackFromInput(InputAction.CallbackContext context)
        {
            StartAttackAnimation();
        }
        public void StartAttackAnimation()
        {
            if (canShoot)
            {
                gearPlaceholder = rangedWeapon;
                canShoot = false;
                Invoke("CooldownReset", rangedWeapon.Cooldown);
                Debug.Log("Attack");
                animator.SetTrigger("Default Attack");
            }
        }
        public void CooldownReset()
        {
            canShoot = true;
            if (playerInputActions.Player.PrimaryAttack.ReadValue<float>() >= InputSystem.settings.defaultButtonPressPoint)
            {
                StartAttackAnimation();
            }
        }
        public void FireAttack()
        {
            if(gearPlaceholder != rangedWeapon)
            {
                Debug.Log("Not firing, weapon switch occured");
                return;
            }
            Debug.Log("Firing Projectile");
            var spawn = Instantiate(rangedWeapon.Projectile, transform.position, Quaternion.identity);
            spawn.transform.rotation = Quaternion.identity;
            spawn.GetComponent<BulletMovement>().BulletSpeed = rangedWeapon.Speed;
            spawn.transform.rotation = GetComponentInParent<Rigidbody>().transform.rotation;
        }
        public void OnDestroy()
        {
            StopAllCoroutines();
            playerInputActions.Player.PrimaryAttack.performed -= StartAttackFromInput;
        }
       

    }

}