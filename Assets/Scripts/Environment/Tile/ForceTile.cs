using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceTile : Tile
{
    public Vector3 forceDirection;

    [Range(1, 10)]
    public float forcePower;
    private float forceMultiplier = 5;
    private List<Rigidbody> bodies;

    public Transform arrow;
    public Transform forceTarget;

    private void Start()
    {
        bodies = new List<Rigidbody>();
        arrow.LookAt(new Vector3(forceDirection.x, 0, forceDirection.z)+arrow.position);
    }
    private void Update()
    {
        if(forceTarget != null)
        {
            forceDirection = forceTarget.position.normalized;
            arrow.LookAt(new Vector3(forceTarget.position.x, 0, forceTarget.position.z));
        }
    }
    private void FixedUpdate()
    {
        foreach(Rigidbody rigidbody in bodies)
        {
            rigidbody.AddForce(forceDirection * forcePower * forceMultiplier, ForceMode.Force);
        }        
    }
    private void OnTriggerEnter(Collider other)
    {        
        var collidedBody = other.GetComponent<Rigidbody>();
        if (!bodies.Contains(collidedBody))
        {
            bodies.Add(collidedBody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bodies.Remove(other.attachedRigidbody);
    }
}
