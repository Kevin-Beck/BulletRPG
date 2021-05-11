using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    InputManager inputManager;
    private Rigidbody myRigidbody;
    private Transform myTransform;
    [SerializeField] float speed = 1;
    [SerializeField] Transform cursorObject;

    private bool sliding = false;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = InputManager.instance;
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
    }

    public void EnabledSliding(float time)
    {
        sliding = true;
        StartCoroutine("DisableSliding");
    }
    private IEnumerator DisableSliding()
    {
        yield return new WaitForSeconds(1f);
        sliding = false;
    }

    private void FixedUpdate()
    {
        myTransform.LookAt(new Vector3(cursorObject.position.x, myTransform.position.y, cursorObject.position.z));

        if (!sliding)
        {
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero;
        }

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

       // Ray ray = new Ray(transform.position, direction);
        // RaycastHit hit;
        //if (!Physics.Raycast(ray, out hit, 0.1f))
        myRigidbody.MovePosition(myRigidbody.position + (direction * speed * Time.deltaTime));
    }
}
