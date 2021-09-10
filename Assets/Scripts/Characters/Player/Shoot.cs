using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletRPG.Characters.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] Transform BulletSpawnPoint;
        [SerializeField] GameObject bullet;
        [SerializeField] float speed;
        [SerializeField] float damageMultiplier;
        PlayerInputActions playerInputActions;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
        }
        private void Start()
        {
            playerInputActions.Player.Enable();
            playerInputActions.Player.Shoot.performed += Fire;
        }
        public void Fire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var spawn = Instantiate(bullet, BulletSpawnPoint.position, Quaternion.identity);
                spawn.transform.rotation = transform.rotation;
                spawn.GetComponent<Rigidbody>().AddForce(speed * spawn.transform.forward);
            }
        }
        public void OnDestroy()
        {
            playerInputActions.Player.Shoot.performed -= Fire;
        }
    }

}