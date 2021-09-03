using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private Transform myTransform;
    [SerializeField] float speed = 1;

    private float capsuleColliderRadius;
    private float radiusBuffer = 0.1f;

    private PlayerInputActions playerInputActions;
    public LayerMask playerPlaneLayer;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        capsuleColliderRadius = GetComponent<CapsuleCollider>().radius + radiusBuffer;
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Vector2 inputDirection = playerInputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

        myRigidbody.velocity = myRigidbody.velocity * 0.9f;
        myRigidbody.angularVelocity = Vector3.zero;

        Ray ray = new Ray(transform.position, direction);
        if (!Physics.Raycast(ray, out RaycastHit hit, capsuleColliderRadius))
        {
            myRigidbody.MovePosition(myRigidbody.position + (direction * speed * Time.deltaTime));
        }
    }
    private void Update()
    {
        //if (inputManager.GetKey(KeybindingActions.Secondary))
        //{
        //    Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out RaycastHit hit, 10000, tileLayerMask))
        //    {
        //        Teleport(hit.point);
        //    }
        //}
        // TODO fix this some? reduce new vector3 constructors etc
        Ray ray = Camera.main.ScreenPointToRay(playerInputActions.Player.MousePosition.ReadValue<Vector2>());
        if(Physics.Raycast(ray, out RaycastHit hit, 100, playerPlaneLayer))
        {
            myTransform.LookAt(new Vector3(hit.point.x, myTransform.position.y, hit.point.z));
        }
        else
        {
            Debug.LogWarning("Player look depends on the player plane existing and being set to the correct layer");
        }
    }

    public void SetSpeedMultiplier(float speedMultiplier, float revertAfterSeconds) // 0 for permanent change
    {
        var currentSpeed = speed;
        speed = speed * speedMultiplier;

        if(revertAfterSeconds > 0)
        {
            StopAllCoroutines();
            StartCoroutine(SetSpeed(currentSpeed, revertAfterSeconds));
        }
    }
    IEnumerator SetSpeed(float speedValue, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        speed = speedValue;
    }
}
