using UnityEngine;
using DG.Tweening;
using BulletRPG.Elements.Projectiles;

public class PlantBullet : BulletBehavior
{
    [SerializeField] GameObject subProjectiles;
    [SerializeField] int numberOfSubProjectiles;
    [SerializeField] GameObject deathParticles;

    void Start()
    {
        Vector3 endPosition = transform.position + new Vector3(0, transform.localScale.y, 0);
        transform.DOMove(endPosition, 1 / BulletSpeed).OnComplete(LaunchProjectiles);
    }
    private void LaunchProjectiles()
    {
        for (int i = 0; i < numberOfSubProjectiles; i++)
        {
            var projectile = Instantiate(subProjectiles, transform.position, Quaternion.identity);
            projectile.transform.rotation *= Quaternion.Euler(0, i * (360 / numberOfSubProjectiles), 0);
        }
        // TODO Explosion
        Destroy(gameObject);
    }

    public override void Update()
    {
        
    }
}
