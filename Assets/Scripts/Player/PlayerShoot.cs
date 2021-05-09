using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform BulletSpawnPoint;
    [SerializeField] Transform Cursor;
    [SerializeField] GameObject bullet;
    [SerializeField] float speed;
    [SerializeField] float damageMultiplier;

    private InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.instance;
    }
    private void Update()
    {
        if(inputManager.GetKeyDown(KeybindingActions.Primary))
        {
            var spawn = Instantiate(bullet, BulletSpawnPoint.position, Quaternion.identity);
            
            spawn.transform.LookAt(Cursor);
            spawn.GetComponent<Rigidbody>().AddForce(speed * spawn.transform.forward);           
        }
    }
}
