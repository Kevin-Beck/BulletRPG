using BulletRPG.NPCBehavior;
using System.Collections;
using UnityEngine;

namespace BulletRPG.NPCBehavior
{
    public class Shoot : NPCBehavior
    {
        public Projectiles projectileGroup;
        public FiringSettings firingSettings;
        public Transform target;

        private Vector3 minPoint;
        private Vector3 maxPoint;
        private int spawnCount = 0;

        [SerializeField] Transform Turret;
        [SerializeField] Transform Barrel;
        [SerializeField] Transform BarrelEnd;

        Coroutine firing;

        void OnEnable()
        {
            firing = StartCoroutine(FireLoop());
        }
        private void OnDisable()
        {
            if (firing != null)
            {
                StopCoroutine(firing);
            }
        }
        void Update()
        {
            Vector3 lookDirectionY = target.position - Turret.position;
            lookDirectionY.y = 0;
            Turret.rotation = Quaternion.RotateTowards(Turret.rotation, Quaternion.LookRotation(lookDirectionY), Time.time * 2);
            
            Vector3 lookDirection = target.position - Barrel.position;
            Barrel.rotation = Quaternion.RotateTowards(Barrel.rotation, Quaternion.LookRotation(lookDirection), Time.time * 2);
        }

        private IEnumerator FireLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(1 / firingSettings.fireRate);

                // get spawn point
                Vector3 spawnPoint;
                if (BarrelEnd == null)
                {
                    spawnPoint = GetSpawnPoint();
                }
                else
                {
                    spawnPoint = BarrelEnd.transform.position;
                }

                // get projectile
                var projectile = Instantiate(projectileGroup.items[Random.Range(0, projectileGroup.items.Count)], spawnPoint, GetProjectileOrientation());
                // send projectile with data
                var rigidBody = projectile.GetComponentInChildren<Rigidbody>();

                Vector3 firingDirection = target.position - BarrelEnd.position;

                rigidBody.AddForce(firingDirection.normalized * 5000 * rigidBody.mass * firingSettings.projectileSpeed);
                rigidBody.AddTorque(transform.up * 2000 * rigidBody.mass * firingSettings.projectileSpin);
                spawnCount++;
            }
        }

        private Quaternion GetProjectileOrientation()
        {
            if (firingSettings.projectileOrientation == ProjectileOrientation.RandomY)
            {
                return Quaternion.Euler(0f, Random.Range(0.0f, 360.0f), 0f);
            }
            else if (firingSettings.projectileOrientation == ProjectileOrientation.Zero)
            {
                return Quaternion.LookRotation(transform.position - target.position);
            }
            else
                return gameObject.transform.rotation;
        }
        private Vector3 GetSpawnPoint()
        {
            maxPoint = transform.TransformPoint(Vector3.right * 10);
            minPoint = transform.TransformPoint(Vector3.left * 10);

            if (firingSettings.firingSequence == FiringSequence.Centered)
            {
                return gameObject.transform.position;
            }
            else if (firingSettings.firingSequence == FiringSequence.Random)
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
            else if (firingSettings.firingSequence == FiringSequence.Sequential)
            {
                int offset = spawnCount % 10;
                return minPoint + transform.TransformPoint(Vector3.right * offset * 2);
            }
            else if (firingSettings.firingSequence == FiringSequence.ReverseSequential)
            {
                int offset = spawnCount % 10;
                offset = 10 - offset;
                return minPoint + transform.TransformPoint(Vector3.right * offset * 2);
            }
            else
                return Vector3.up;
        }
    }
}