using BulletRPG.Gear.Weapons.RangedWeapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : BulletBehavior
{
    [SerializeField] GameObject subProjectiles;
    [SerializeField] int numberOfSubProjectiles;
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float popHeight;
    Vector3 startPosition;
    Vector3 endPosition;
    float startTime;

    void Start()
    {
        startTime = Time.time;
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(0, popHeight, 0);
    }

    public override void Update()
    {
        float distanceCovered = (Time.time - startTime) * BulletSpeed;
        float fractionCovered = distanceCovered / popHeight;
        transform.position = Vector3.Lerp(startPosition, endPosition, fractionCovered);
        if(fractionCovered > .95)
        {
            Debug.Log("ShootOff particles");
            for(int i = 0; i < numberOfSubProjectiles; i++)
            {
                var projectile = Instantiate(subProjectiles, transform.position, Quaternion.identity);
                projectile.transform.rotation *= Quaternion.Euler(0, i * (360 / numberOfSubProjectiles), 0);
            }
            // TODO Explosion
            Destroy(gameObject);
        }
    }

}
