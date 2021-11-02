using BulletRPG;
using BulletRPG.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseMovement : MonoBehaviour, INPCMove
{
    [SerializeField] MovementType movementType;
    private Transform targetTransform;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    private Animator animator;

    private float baseSpeed;
    private float baseAcceleration;
    private Coroutine speedChange = null;

    private Vector3 previousPosition;
    private Vector3 currentPostition;
    private Vector3 changeInPosition;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = false;

        currentPostition = transform.position;
        changeInPosition = Vector3.zero;
        previousPosition = transform.position;

        targetTransform = Utilities.GetPlayerTransform();
    }

    public void Move()
    {     
        var movement = GetMovement(movementType);
        movement();
        Animate();       
    }
    private Action GetMovement(MovementType movementType) => movementType switch
    {
        MovementType.RandomPositions => () => RandomPositions(),
        MovementType.FollowPlayer => () => FollowPlayer(),
        _ => () => { Debug.LogWarning("Action not defined for Movement type"); },
    };
    private void Animate()
    {
        previousPosition = currentPostition;
        currentPostition = transform.position;
        changeInPosition = currentPostition - previousPosition;
        changeInPosition = transform.InverseTransformDirection(changeInPosition);

        animator.SetFloat("velocityx", changeInPosition.normalized.x);
        animator.SetFloat("velocityz", changeInPosition.normalized.z);
    }
    private void FollowPlayer()
    {
        navMeshAgent.destination = targetTransform.position;
    }
    private void RandomPositions()
    {
        if (PathComplete() || navMeshAgent.destination == null)
        {
            navMeshAgent.destination = GetRandomDestination();
        }
    }

    protected bool PathComplete()
    {
        if (Vector3.Distance(navMeshAgent.destination, navMeshAgent.transform.position) <= navMeshAgent.stoppingDistance)
        {
            if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }
    public void Stop()
    {
        navMeshAgent.destination = transform.position;
        animator.SetBool("moving", false);
    }
    public void SetSpeedAndAcceleration(float speed, float acceleration)
    {
        navMeshAgent.speed = speed;
        navMeshAgent.acceleration = acceleration;
    }

    public void SetSpeedMultiplier(float speedMultiplier, float revertAfterSeconds)
    {
        if (speedChange != null)
        {
            StopCoroutine(speedChange);
        }
        var newSpeed = navMeshAgent.speed * speedMultiplier;
        var newAcceleration = navMeshAgent.acceleration * speedMultiplier * 1.5f;

        navMeshAgent.acceleration = newAcceleration;
        navMeshAgent.speed = newSpeed;

        speedChange = StartCoroutine(SetSpeed(baseSpeed, baseAcceleration, revertAfterSeconds));
    }

    IEnumerator SetSpeed(float speedValue, float accelerationValue, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        navMeshAgent.speed = speedValue;
        navMeshAgent.acceleration = accelerationValue;
    }
    private Vector3 GetRandomDestination()
    {
        Vector3 position = Vector3.zero;
        for(int i = 0; i < 10; i++)
        {
            if (!Utilities.GetRandomPointOnNavMesh(Vector3.zero, 10f, out position))
            {
                Debug.Log("Warning random point not found");
            }
            if((position - transform.position).magnitude > 5)
            {
                return position;
            }
        }
        return position;
    }
}

public enum MovementType
{
    FollowPlayer,
    RandomPositions
}
