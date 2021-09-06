using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public LayerMask playerLookLayer;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    private Animator animator;
    public string animationBlendTreeParameterName;
    public string animationForwardAxisName;
    public string animationRightAxisName;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    private void Start()
    {
        animator.SetBool(animationBlendTreeParameterName, true);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
        if(Physics.Raycast(ray, out RaycastHit hit, 100, playerLookLayer))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
        else
        {
            Debug.LogWarning("Raycast for player look direction not hitting anything on PlayerLookLayer");
        }

        // Get input from player, create a vector3
        Vector2 inputDirection = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

        // Set destination
        navMeshAgent.destination = transform.position + moveDirection * 2f;

        if(animator != null)
        {
            // Adjust vector based on current rotation to create correct animation
            moveDirection = transform.InverseTransformDirection(moveDirection);
            animator.SetFloat(animationRightAxisName, moveDirection.x);
            animator.SetFloat(animationForwardAxisName, moveDirection.z);
        }
    }
}
