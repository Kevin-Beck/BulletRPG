using BulletRPG;
using BulletRPG.Gear.Weapons.RangedWeapons;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrackingBullet : BulletBehavior
{
    [SerializeField] private float startToLandingTime;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float accelerationTime = 5f;
    [SerializeField] private float missileSpeed = 5f;
    [SerializeField] private float turnRate = 50f;
    private bool isTracking = true;
    [SerializeField] private float aliveTime = 6f;
    [SerializeField] private float aliveTimer;
    
    private bool isAccelerating = true;
    private float accelerationTimer = 0f;
    private bool chase = false;

    private Rigidbody myRigidbody;
    private Quaternion guideRotation;

    [SerializeField] Transform target;
    [SerializeField] bool trackPlayer;
    [SerializeField] GameObject trackingLostParticles;

    void Start()
    {
        if(aliveTime == 0f)
        {
            Debug.LogWarning($"ALIVE TIMER IS SET TO 0 ON ${gameObject} Bullet");
        }
        myRigidbody = GetComponent<Rigidbody>();

        if (trackPlayer)
        {
            target = Utilities.GetPlayerTransform();
        }
        Vector3 landingPosition = transform.position + transform.forward * 3f;
        landingPosition.y = 1f;
        transform.DOMove(landingPosition, startToLandingTime).SetEase(Ease.InOutCirc).OnComplete(() => 
        { 
            chase = true;
            aliveTimer = Time.time;
            accelerationTime = Time.time;
        });
    }
    // Update is called once per frame
    public override void Update()
    {
        if (chase)
        {
            Run();
        }
    }
    private void Run()
    {
        if(Since(aliveTimer) > aliveTime)
        {
            Destroy(gameObject);
            return;
        }
        if (Since(accelerationTimer) > accelerationTime)
        {
            isAccelerating = false;
        }
        else
        {
            isAccelerating = true;
        }

        if (isAccelerating)
        {
            missileSpeed += acceleration * Time.deltaTime;
        }
        myRigidbody.velocity = transform.forward * missileSpeed;

        if (isTracking)
        {
            Vector3 relativePosition = target.position - transform.position;
            guideRotation = Quaternion.LookRotation(relativePosition, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, guideRotation, turnRate * Time.deltaTime);
            if(Vector3.SqrMagnitude(relativePosition) < 6f)
            {
                isTracking = false;
                if (trackingLostParticles != null)
                {
                    Instantiate(trackingLostParticles, transform.position, Quaternion.LookRotation(GetComponent<Rigidbody>().velocity * -1, transform.up));
                }
            }
        }
    }
    private float Since(float since)
    {
        return Time.time - since;
    }
    private void OnDestroy()
    {
    }
}
