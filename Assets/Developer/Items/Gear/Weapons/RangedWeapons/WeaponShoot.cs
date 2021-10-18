using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletRPG.Gear.Weapons.RangedWeapons
{
    public class WeaponShoot : MonoBehaviour, IAttack
    {
        [SerializeField] RangedWeapon rangedWeapon;
        private RangedWeapon gearPlaceholder;
        private bool canShoot = true;

        PlayerInputActions playerInputActions;
        Animator animator;

        private void Awake()
        {
            canShoot = false;
            Invoke("CooldownReset", rangedWeapon.reloadTime);

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
                Invoke("CooldownReset", rangedWeapon.reloadTime);
                Debug.Log("Attack");
                animator.SetTrigger("Default Attack");
            }
        }
        public void SetWeapon(RangedWeapon weapon)
        {
            rangedWeapon = weapon;
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
            var bullet = Instantiate(rangedWeapon.projectile, transform.position + transform.up, Quaternion.identity);
            bullet.transform.rotation = Quaternion.identity;
            var bulletSettings = bullet.GetComponent<BulletBehaviour>();
            bulletSettings.BulletSpeed = rangedWeapon.projectileSpeed;
            bulletSettings.damage = rangedWeapon.damage.GetDamage();

            bullet.transform.rotation = GetComponentInParent<Rigidbody>().transform.rotation;
        }
        public void OnDestroy()
        {
            StopAllCoroutines();
            playerInputActions.Player.PrimaryAttack.performed -= StartAttackFromInput;
        }
    }
}