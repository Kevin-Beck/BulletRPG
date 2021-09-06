using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Transform myTransform;
    [SerializeField] float speed = 1;

    private float capsuleColliderRadius;
    private float radiusBuffer = 0.1f;

    private PlayerInputActions playerInputActions;
    public LayerMask playerLookLayer;
    private NavMeshAgent navMeshAgent;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("IsFlying", true);
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        capsuleColliderRadius = GetComponent<CapsuleCollider>().radius + radiusBuffer;
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Vector2 inputDirection = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

        myRigidbody.velocity = myRigidbody.velocity * 0.9f;
        myRigidbody.angularVelocity = Vector3.zero;
        
        Ray ray = new Ray(transform.position, moveDirection);
        if (!Physics.Raycast(ray, out RaycastHit hit, capsuleColliderRadius))
        {
            myRigidbody.MovePosition(myRigidbody.position + (moveDirection * speed * Time.deltaTime));

            Vector3 look = transform.forward;
            Vector3 move = moveDirection;

            moveDirection = transform.InverseTransformDirection(moveDirection);
            animator.SetFloat("FlyX", moveDirection.x);
            animator.SetFloat("FlyY", moveDirection.z);
        }
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
        if(Physics.Raycast(ray, out RaycastHit hit, 100, playerLookLayer))
        {
            myTransform.LookAt(new Vector3(hit.point.x, myTransform.position.y, hit.point.z));
        }
    }
}
