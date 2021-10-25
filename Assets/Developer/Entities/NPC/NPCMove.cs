using BulletRPG;
using BulletRPG.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMove : MonoBehaviour, IMove
{
    [HideInInspector] public NavMeshAgent navMeshAgent;
    private Animator animator;
    private Rigidbody rigidB;

    private float baseSpeed;
    private float baseAcceleration;
    private Coroutine speedChange = null;

    private bool _moveAtRandom = false;
    private bool _moveToPosition = false;
    private bool _isMoving = false;
    public bool MoveAtRandom { get => _moveAtRandom; set => _moveAtRandom = value; }
    public bool MoveToPosition { get => _moveToPosition; set => _moveToPosition = value; }
    public bool IsMoving { get => _isMoving; }

    private Vector3 previousPosition;
    private Vector3 currentPostition;
    private Vector3 changeInPosition;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    public void Start()
    {
        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = false;

        currentPostition = transform.position;
        changeInPosition = Vector3.zero;
        previousPosition = transform.position;
    }
    public void Update()
    {
        if (navMeshAgent.destination != null && IsMoving)
        {
            previousPosition = currentPostition;
            currentPostition = transform.position;
            changeInPosition = currentPostition - previousPosition;
            changeInPosition = transform.InverseTransformDirection(changeInPosition);
            
            animator.SetFloat("velocityx", changeInPosition.normalized.x);
            animator.SetFloat("velocityz", changeInPosition.normalized.z);

            transform.LookAt(navMeshAgent.destination);

            if (pathComplete())
            {
                if (_moveAtRandom)
                {
                    navMeshAgent.destination = GetRandomDestination();
                }
                else
                {
                    Stop();
                }
            }
        }
    }
    public void Die()
    {
        _isMoving = false;
    }
    protected bool pathComplete()
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
    private void Stop()
    {
        _isMoving = false;
        animator.SetBool("moving", false);
    }
    public void MoveTowardsRandomPositions(float time)
    {
        _moveAtRandom = true;
        Invoke("Stop", time);
        navMeshAgent.destination = GetRandomDestination();
        _isMoving = true;
        animator.SetBool("moving", true);
    }
    public void MoveToRandomPosition()
    {
        _moveToPosition = true;
        navMeshAgent.destination = GetRandomDestination();
        _isMoving = true;
        animator.SetBool("moving", true);
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
