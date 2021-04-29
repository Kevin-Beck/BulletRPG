using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletRPG.NPCBehavior;

public class Movement : NPCBehavior
{
    public Pattern pattern;
    public float speed;
    private int currentWaypoint = 0;


    private void Start()
    {
        transform.position = pattern.waypoints[0];
    }
    private void Update()
    {
        // If we're close, get next point
        if (Vector3.Distance(transform.position, pattern.waypoints[currentWaypoint]) < .1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= pattern.waypoints.Count)
            {
                // if we're out of points start over
                currentWaypoint = 0;
            }
        }
        // walk towards point
        transform.position = Vector3.MoveTowards(transform.position, pattern.waypoints[currentWaypoint], Time.deltaTime * speed);
    }
}
