using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCShoot : MonoBehaviour
{
    public ProjectileGroup projectileGroup;
    public FiringSettings firingSettings;

    [SerializeField] public bool delayStart;
    [SerializeField] public float startAfterDelaySeconds;
    [SerializeField] public bool stopAfter;
    [SerializeField] public float stopAfterSeconds;

    private Vector3 minPoint;
    private Vector3 maxPoint;
    private int spawnCount = 0;

    void Start()
    {
        
        StartCoroutine(FireLoop());
        if (stopAfter)
        {
            Destroy(this, stopAfterSeconds);
        }
    }
    private void Update()
    {
        gameObject.transform.LookAt(Vector3.zero);
    }

    private IEnumerator FireLoop()
    {
        if (delayStart)
        {
            yield return new WaitForSeconds(startAfterDelaySeconds);
        }
        while (true)
        {
            yield return new WaitForSeconds(1 / firingSettings.fireRate);
            
            // get spawn point            
            Vector3 spawnPoint = GetSpawnPoint(); 
            // get projectile
            var projectile = Instantiate(projectileGroup.projectiles[Random.Range(0, projectileGroup.projectiles.Count)], spawnPoint, GetProjectileOrientation());
            // send projectile with data
            var rigidBody = projectile.GetComponentInChildren<Rigidbody>();
            rigidBody.AddForce(transform.forward * 5000 * rigidBody.mass * firingSettings.projectileSpeed);
            rigidBody.AddTorque(transform.up * 2000 * rigidBody.mass * firingSettings.projectileSpin);
            spawnCount++;
        }
    }

    private Quaternion GetProjectileOrientation()
    {
        if(firingSettings.projectileOrientation == ProjectileOrientation.RandomY)
        {
            return Quaternion.Euler(0f, Random.Range(0.0f, 360.0f), 0f);
        }else if(firingSettings.projectileOrientation == ProjectileOrientation.Zero)
        {
            return gameObject.transform.rotation;
        }else
            return gameObject.transform.rotation;
    }
    private Vector3 GetSpawnPoint()
    {
        maxPoint = transform.TransformPoint(Vector3.right * 10);
        minPoint = transform.TransformPoint(Vector3.left * 10);
        
        if(firingSettings.firingSequence == FiringSequence.Centered)
        {
            return gameObject.transform.position;
        }else if (firingSettings.firingSequence == FiringSequence.Random)
        {
            return minPoint + Random.Range(0f, 1f) * (maxPoint - minPoint);
        }
        else if (firingSettings.firingSequence == FiringSequence.SineWave)
        {
            return transform.TransformPoint(Vector3.right * Mathf.Sin(spawnCount * (Mathf.PI / 8)) * 10);
        }
        else if (firingSettings.firingSequence == FiringSequence.CosWave)
        {
            return transform.TransformPoint(Vector3.right * Mathf.Cos(spawnCount * (Mathf.PI / 8)) * 10);
        }
        else if(firingSettings.firingSequence == FiringSequence.Sequential)
        {
            int offset = spawnCount % 10;
            return minPoint + transform.TransformPoint(Vector3.right * offset * 2);
        }else if(firingSettings.firingSequence == FiringSequence.ReverseSequential)
        {
            int offset = spawnCount % 10;
            offset = 10 - offset;
            return minPoint + transform.TransformPoint(Vector3.right * offset * 2);
        }
        else
            return Vector3.up;
    }
}
