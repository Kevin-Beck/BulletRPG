using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnerConfig spawnerConfig;
    [SerializeField] private float delay;

    private Vector3 minPoint;
    private Vector3 maxPoint;
    private int spawnCount = 0;

    void Start()
    {
        maxPoint = transform.TransformPoint(Vector3.right * 10);
        minPoint = transform.TransformPoint(Vector3.left * 10);
        StartCoroutine(FireLoop());
    }

    private IEnumerator FireLoop()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            yield return new WaitForSeconds(1 / spawnerConfig.firingSettings.fireRate);
            
            // get spawn point            
            Vector3 spawnPoint = GetSpawnPoint(); 
            // get projectile
            var projectile = Instantiate(spawnerConfig.projectileGroup.projectiles[Random.Range(0, spawnerConfig.projectileGroup.projectiles.Count)], spawnPoint, transform.rotation);
            // send projectile with data
            var rigidBody = projectile.GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.forward * 5000 * spawnerConfig.firingSettings.projectileSpeed);
            rigidBody.AddTorque(transform.up * 2000 * spawnerConfig.firingSettings.projectileSpin);
            spawnCount++;
        }
    }
    private Vector3 GetSpawnPoint()
    {
        if (spawnerConfig.firingSettings.firingSequence == FiringSequence.Random)
        {
            return minPoint + Random.Range(0f, 1f) * (maxPoint - minPoint);
        }
        else if (spawnerConfig.firingSettings.firingSequence == FiringSequence.SineWave)
        {
            return transform.TransformPoint(Vector3.right * Mathf.Sin(spawnCount * (Mathf.PI / 8)) * 10);
        }
        else if (spawnerConfig.firingSettings.firingSequence == FiringSequence.CosWave)
        {
            return transform.TransformPoint(Vector3.right * Mathf.Cos(spawnCount * (Mathf.PI / 8)) * 10);
        }
        else if(spawnerConfig.firingSettings.firingSequence == FiringSequence.Sequential)
        {
            int offset = spawnCount % 10;
            return minPoint + transform.TransformPoint(Vector3.right * offset * 2);
        }else if(spawnerConfig.firingSettings.firingSequence == FiringSequence.ReverseSequential)
        {
            int offset = spawnCount % 10;
            offset = 10 - offset;
            return minPoint + transform.TransformPoint(Vector3.right * offset * 2);
        }
        else
            return Vector3.up;
    }
}
