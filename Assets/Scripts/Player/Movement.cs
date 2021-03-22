using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    InputManager inputManager;
    private Rigidbody myRigidbody;
    private Transform myTransform;
    [SerializeField] float speed = 1;
    [SerializeField] Transform cursorObject;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.instance;
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        myTransform.LookAt(new Vector3(cursorObject.position.x, myTransform.position.y, cursorObject.position.z));

        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;

        Vector3 direction = Vector3.zero;
        if (inputManager.GetKey(KeybindingActions.North))
        {
            direction += Vector3.forward;
        }
        if (inputManager.GetKey(KeybindingActions.East))
        {
            direction += Vector3.right;
        }
        if (inputManager.GetKey(KeybindingActions.South))
        {
            direction += Vector3.back;
        }
        if (inputManager.GetKey(KeybindingActions.West))
        {
            direction += Vector3.left;
        }
        direction = direction.normalized;
        myRigidbody.MovePosition(myRigidbody.position + (direction * speed * Time.deltaTime));
    }
}
